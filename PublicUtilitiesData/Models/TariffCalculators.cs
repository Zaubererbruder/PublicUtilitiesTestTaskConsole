using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PublicUtilitiesData.Models
{
    public class TariffCalculators
    {
        private TariffCalculators() { }

        public TariffCalculators(Tariff tariff, Calculator calculator)
        {
            Tariff = tariff;
            Calculator = calculator;
        }

        [Key]
        [ForeignKey(nameof(Tariff))]
        public int TariffId { get; private set; }
        public int CalculatorId { get; private set; }
        public Tariff Tariff { get; private set; }
        public Calculator Calculator { get; private set; }
    }
}
