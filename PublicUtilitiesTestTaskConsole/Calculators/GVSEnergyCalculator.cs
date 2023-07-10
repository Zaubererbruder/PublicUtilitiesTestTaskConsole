using PublicUtilitiesData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
