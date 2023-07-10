using System;
using System.Linq;

namespace PublicUtilitiesData
{
    public class DefaultCalculator : ICalculator
    {
        public virtual void Calculate(CommonData commonData, CalculationRow row)
        {
            CalculationResult calculationResult = new CalculationResult();

            calculationResult.Volume = CalculateVolume(commonData, row);

            calculationResult.Summ = Math.Round(CalculateSumm(calculationResult.Volume, commonData, row), 2, MidpointRounding.AwayFromZero);

            row.AddCalculations(calculationResult);
        }

        public virtual decimal CalculateVolume(CommonData commonData, CalculationRow row)
        {
            if (row.MetersData.Count(md => md.MetersValue == null) > 0)
                return CalculateVolumeWithoutMetersData(commonData, row);
            else
                return CalculateVolumeWithMetersData(commonData, row);

        }

        public virtual decimal CalculateVolumeWithMetersData(CommonData commonData, CalculationRow row)
        {
            var lastMetersData = row.LastMetersData.Where(md => md.MetersType == row.Service.MetersType).FirstOrDefault();
            var metersData = row.MetersData.Where(md => md.MetersType == row.Service.MetersType).FirstOrDefault();

            decimal lastMetersValue = lastMetersData?.MetersValue ?? 0;
            decimal metersValue = metersData?.MetersValue ?? 0;

            return metersValue - lastMetersValue;
        }

        public virtual decimal CalculateVolumeWithoutMetersData(CommonData commonData, CalculationRow row)
        {
            return commonData.PeoplesCountInMonth.Value * row.Tariff.Standart;
        }

        public virtual decimal CalculateSumm(decimal volume, CommonData commonData, CalculationRow row)
        {
            return volume * row.Tariff.Cost;
        }
    }
}
