using PublicUtilitiesData;
using System.Collections.Generic;

namespace PublicUtilitiesTestTaskConsole.Calculators
{
    internal class CalculatorsLoader : ICalculatorsLoader
    {
        Dictionary<int, ICalculator> ICalculatorsLoader.GetCalculators()
        {
            Dictionary<int, ICalculator> calculators = new Dictionary<int, ICalculator>();
            calculators.Add(1, new DefaultCalculator());
            calculators.Add(2, new GVSEnergyCalculator());
            calculators.Add(3, new EECalculator());

            return calculators;
        }
    }
}
