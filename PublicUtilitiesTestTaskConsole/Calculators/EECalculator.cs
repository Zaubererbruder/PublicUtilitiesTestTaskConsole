using PublicUtilitiesData;

namespace PublicUtilitiesTestTaskConsole.Calculators
{
    public class EECalculator : DefaultCalculator
    {
        public override decimal CalculateVolumeWithoutMetersData(CommonData commonData, CalculationRow row)
        {
            if (row.Service.MetersType != null)
                return 0;

            return base.CalculateVolumeWithoutMetersData(commonData, row);
        }
    }
}
