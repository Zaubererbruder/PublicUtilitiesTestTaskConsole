using System.Collections.Generic;

namespace PublicUtilitiesData
{
    public class DefaultCalculatorsLoader : ICalculatorsLoader
    {
        public Dictionary<int, ICalculator> GetCalculators()
        {
            Dictionary<int, ICalculator> calculators = new Dictionary<int, ICalculator>();
            calculators.Add(1, new DefaultCalculator());
            return calculators;
        }
    }
}
