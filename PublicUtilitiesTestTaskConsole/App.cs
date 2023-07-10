using PublicUtilitiesData;
using PublicUtilitiesData.Models;
using PublicUtilitiesTestTaskConsole.Calculators;
using System;
using System.Collections.Generic;

namespace PublicUtilitiesTestTaskConsole
{
    public class App
    {
        private PublicUtilities _publicUtilities;

        private Consumer _consumer;
        private DateTime _period;
        private ChangingValue _peoplesCount;
        private Dictionary<MetersType, decimal?> _metersData;
        
        public void Start()
        {
            _publicUtilities = PublicUtilities
                    .Create("Data Source = pu.db")
                    .WithCalculatorLoader(new CalculatorsLoader())
                    .WithStartDataLoader(new StartDataLoader())
                    .Init();

            SetAccNumber();

            while (true)
            {     
                SetPeriod();
                InputPeoplesCountChange();
                InputMetersData();

                using (var calcFactory = _publicUtilities.Calculations
                            .Create(_consumer, _period)
                            .SetPeoplesCount(_peoplesCount)
                            .AddMetersData(_metersData))
                {
                    var calculation = calcFactory.Calculate().GetAwaiter().GetResult();
                    OutputCalculation(calculation);

                }

                Console.WriteLine("Нажмите для продолжения");
                Console.ReadKey();
            }
        }

        private void SetAccNumber()
        {
            var accNumber = 1;
            _consumer = Consumer.GetInstance(accNumber);
        }

        private void SetPeriod()
        {
            _period = CalculationsHistory.GetNextFreePeriod(_consumer);
            Console.WriteLine($"Заполнение за период {_period:MM.yyyy}");
        }

        private void InputPeoplesCountChange()
        {
            var lastPeoplesCount = PeopleCountHistory.GetPeopleCountOnStartOfPeriod(_consumer, _period);
            var _peoplesCountBuilder = ChangingValue.Create(_period, lastPeoplesCount);
            var daysInMonth = DateTime.DaysInMonth(_period.Year, _period.Month);

            Console.WriteLine($"Количество проживающих людей в помещении на конец прошлого периода - {lastPeoplesCount}");
            for (int i = 0; i < daysInMonth;)
            {
                Console.WriteLine("Укажите день в который изменилось количество человек проживающих в помещении. Если не изменялось введите \'-\'...");
                string changesDayInput = Console.ReadLine();
                if (changesDayInput == "-")
                {
                    _peoplesCount = _peoplesCountBuilder.Build();
                    return;
                }

                if (!int.TryParse(changesDayInput, out int changesDay)
                    || changesDay < 0
                    || changesDay > daysInMonth)
                {
                    Console.WriteLine("Неверный ввод дня изменения количества человек");
                    continue;
                }

                DateTime changesDate = new DateTime(_period.Year, _period.Month, changesDay);
                if(changesDate < _peoplesCountBuilder.LastDate 
                    || (changesDate == _peoplesCountBuilder.LastDate && changesDay != 1))
                {
                    Console.WriteLine("Неверный ввод дня изменения количества человек. Новый день изменения должен быть больше предыдущего");
                    continue;
                }

                var newPeopleCount = InputPeoplesCount();
                _peoplesCountBuilder = _peoplesCountBuilder.WithChange(changesDate, newPeopleCount);
                i = changesDay;
            }

            _peoplesCount = _peoplesCountBuilder.Build();
        }

        private int InputPeoplesCount()
        {
            Console.WriteLine("Введите новое количество человек");
            var peoplesCountInput = Console.ReadLine();

            if (!int.TryParse(peoplesCountInput, out int peoplesCount)
                || peoplesCount < 0)
            {
                Console.WriteLine("Неверный ввод нового количетсва человек");
                InputPeoplesCount();
            }

            return peoplesCount;
        }

        private void InputMetersData()
        {
            _metersData = new Dictionary<MetersType, decimal?>();

            Console.WriteLine("Введите показания счетчиков. Если не имеется вводите \'-\'");
            List<MetersType> metersTypes = new List<MetersType>(MetersType.GetAll());

            for (var i = 0; i < metersTypes.Count; i++)
            {
                var meterType = metersTypes[i];

                Console.WriteLine($"Введите показания за {meterType}...");
                string metersValueInput = Console.ReadLine();
                if (metersValueInput == "-")
                {
                    _metersData.Add(meterType, null);
                    continue;
                }

                if (!decimal.TryParse(metersValueInput, out decimal metersValue)
                    || metersValue < 0)
                {
                    Console.WriteLine("Неверный ввод данных счетчика. Допускается ввод не отрицательного числа или символа \'-\'");
                    i--;
                    continue;
                }

                _metersData.Add(meterType, metersValue);
            }
        }

        private void OutputCalculation(Calculation calculation)
        {
            Console.WriteLine($"Рассчитанные данные за расчетный период {calculation.Period:MM.yyyy}. Количество проживающих человек: {_peoplesCount}");
            
            Console.Write("{0,25}|", "Виды услуг");
            Console.Write("{0,10}|", "Ед. изм");
            Console.Write("{0,10}|", "Норматив");
            Console.Write("{0,10}|", "Объем");
            Console.Write("{0,10}|", "Тариф");
            Console.WriteLine("{0,10}", "Сумма");

            foreach (CalculationRow row in calculation.Rows)
            {
                Console.Write("{0,25}|", row.Service.Name);
                Console.Write("{0,10}|", row.Service.Unit);
                Console.Write("{0,10}|", row.Tariff.Standart);
                Console.Write("{0,10}|", Math.Round(row.CalculationResult.Volume, 5));
                Console.Write("{0,10}|", row.Tariff.Cost);
                Console.WriteLine("{0,10}", row.CalculationResult.Summ);
            }

            Console.WriteLine($"Итоговая сумма: {calculation.Summ}");
            Console.WriteLine("");
        }
    }
}
