using Microsoft.EntityFrameworkCore;
using PublicUtilitiesData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicUtilitiesData
{
    public class CalculationFactory : IDisposable
    {
        private Consumer _consumer;
        private DateTime _period;
        private IDictionary<int, ICalculator> _calculators;
        private IDictionary<Tariff, ICalculator> _tariffCalculators;
        private CommonData _commonData;
        private Dictionary<MetersType, decimal?> _meters = new Dictionary<MetersType, decimal?>();
        private PUDbContext _context;

        internal CalculationFactory(Consumer consumer, DateTime period, IDictionary<int, ICalculator> calculators)
        {
            _consumer = consumer;
            _period = period;
            _commonData = new CommonData();
            _calculators = calculators;
            _context = new PUDbContext();
            _context.Consumers.Attach(_consumer);
        }

        public CalculationFactory SetPeoplesCount(ChangingValue count)
        {
            _commonData.PeoplesCountInMonth = count;
            return this;
        }

        public CalculationFactory AddMetersData(IDictionary<MetersType, decimal?> data)
        {
            _meters = new Dictionary<MetersType, decimal?>(data);

            _context.MetersTypes.AttachRange(_meters.Keys);

            return this;
        }

        public async Task<Calculation> Calculate()
        {
            await LoadCalculators();
            await AddPeoplesCountChangesInDB();
            await AddMetersDataInDB();

            List<CalculationRow> rows = new List<CalculationRow>();

            foreach (var tariff in _tariffCalculators.Keys)
            {
                Service service = tariff.Service;
                ICalculator calculator = _tariffCalculators[tariff];

                CalculationRow calcRow = new CalculationRow(tariff);

                List<MetersData> metersDataList = new List<MetersData>();
                List<MetersData> lastMetersDataList = new List<MetersData>();
                foreach (var metersType in _meters.Keys)
                {
                    if (metersType.ServiceBase.Id != service.ServiceBase.Id)
                        continue;

                    var metersData = await _context.MetersData.FindAsync(_period, _consumer.AccountNumber, metersType.Id);
                    var lastMetersData = await _context.MetersData.FindAsync(_period.AddMonths(-1), _consumer.AccountNumber, metersType.Id);

                    metersDataList.Add(metersData);

                    if (lastMetersData != null)
                        lastMetersDataList.Add(lastMetersData);
                }

                calcRow.AddMetersData(metersDataList, lastMetersDataList);

                calculator.Calculate(_commonData, calcRow);

                CalculationsHistory calculationsHistory = new CalculationsHistory(_consumer, _period, service, calcRow.CalculationResult.Summ);
                _context.Add(calculationsHistory);

                rows.Add(calcRow);
            }


            await _context.SaveChangesAsync();

            Calculation calculation = new Calculation(
                _consumer,
                _period,
                _commonData,
                rows);

            return calculation;
        }

        private async Task AddPeoplesCountChangesInDB()
        {
            int lastPeoplesCount = PeopleCountHistory.GetPeopleCountOnStartOfPeriod(_consumer, _period);
            for (int i = 0; i < _commonData.PeoplesCountInMonth.Changes.Count; i++)
            {
                var change = _commonData.PeoplesCountInMonth.Changes[i];

                if(change.Value != lastPeoplesCount)
                {
                    await _context.AddAsync(new PeopleCountHistory(
                        _consumer,
                        change.Period,
                        (int)change.Value));
                }
            }
        }

        private async Task AddMetersDataInDB()
        {
            foreach (var metersType in _meters.Keys)
            {
                MetersData metersData = new MetersData(_period, _consumer, metersType, _meters[metersType]);
                await _context.AddAsync(metersData);
            }
        }

        private async Task LoadCalculators()
        {
            _tariffCalculators = new Dictionary<Tariff, ICalculator>();

            var serviceTariffs = _context.ConsumerTariffs
                .Include(st => st.Tariff)
                .Include(st => st.Tariff.Service)
                .Include(st => st.Tariff.Service.Unit)
                .Include(st => st.Tariff.Service.ServiceBase)
                .Include(st => st.Tariff.Service.ServiceMetersType);

            var tariffCalculators =
            await (from st in _context.ConsumerTariffs
             join tc in _context.TariffCalculators
                on st.TariffId equals tc.TariffId
                into ServiceCalculators
             from sc in ServiceCalculators.DefaultIfEmpty()
             select new
             {
                 st.Tariff,
                 CalculatorId = sc != null ? sc.CalculatorId : 1
             })
                .ToDictionaryAsync(tc => tc.Tariff, tc => tc.CalculatorId);

            foreach (var serviceCalculator in serviceTariffs)
            {
                int calcId = tariffCalculators[serviceCalculator.Tariff];
                _tariffCalculators.Add(serviceCalculator.Tariff, _calculators[calcId]);
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}