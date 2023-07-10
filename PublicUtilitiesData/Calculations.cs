using PublicUtilitiesData.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PublicUtilitiesData
{
    public class Calculations
    {
        private Dictionary<int, ICalculator> _calculators;

        internal Calculations(Dictionary<int, ICalculator> calculators) 
        {
            _calculators = calculators;
        }

        public CalculationFactory Create(Consumer consumer, DateTime period)
        {
            using (var context = new PUDbContext())
            {
                if (consumer == null)
                    throw new ArgumentNullException(nameof(consumer));

                CalculationsHistory existingCalculationsHistory = context.CalculationsHistory
                    .Where(item => item.Consumer == consumer && item.Period == period)
                    .FirstOrDefault();

                if (existingCalculationsHistory != null)
                    throw new ArgumentException("Cannot provide calculations for this period. Data already exist.");
            }
            CalculationFactory calculation = new CalculationFactory(consumer, period, _calculators);
            return calculation;
        }
    }
}
