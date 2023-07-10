using PublicUtilitiesData;

namespace PublicUtilitiesTestTaskConsole.Calculators
{
    internal class GVSEnergyCalculator : DefaultCalculator
    {
        public override decimal CalculateVolume(CommonData commonData, CalculationRow row)
        {
            return base.CalculateVolume(commonData, row) * row.Tariff.Standart;
        }
    }
}
