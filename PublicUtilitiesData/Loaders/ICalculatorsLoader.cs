using System.Collections.Generic;

namespace PublicUtilitiesData
{
    public interface ICalculatorsLoader
    {
        Dictionary<int, ICalculator> GetCalculators();
    }
}
