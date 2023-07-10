using PublicUtilitiesData.Models;
using System.Collections.Generic;

namespace PublicUtilitiesData
{
    public class CalculationRow
    {
        internal CalculationRow(Tariff tariff)
        {
            Tariff = tariff;
            Service = Tariff.Service;
            ServiceBase = Service.ServiceBase;
        }

        public ServiceBase ServiceBase { get; set; }
        public Service Service { get; private set; }
        public Tariff Tariff { get; private set; }
        public List<MetersData> LastMetersData { get; private set; } = new List<MetersData>();
        public List<MetersData> MetersData { get; private set; } = new List<MetersData>();
        public CalculationResult CalculationResult { get; private set; }

        internal void AddMetersData(List<MetersData> metersData, List<MetersData> lastMetersData)
        {
            MetersData = metersData;
            LastMetersData = lastMetersData;
        }

        internal void AddCalculations(CalculationResult result)
        {
            CalculationResult = result;
        }
    }
}
