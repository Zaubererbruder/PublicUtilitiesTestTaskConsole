using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PublicUtilitiesData.Models
{
    public class ConsumerTariff
    {
        private ConsumerTariff() { }
        public ConsumerTariff(Consumer consumer, Service service, Tariff tariff)
        {
            Consumer = consumer;
            ConsumerId = consumer.AccountNumber;
            Service = service;
            ServiceId = service.Id;
            Tariff = tariff;
            TariffId = tariff.Id;
        }

        public int ConsumerId { get; private set; }
        public int ServiceId { get; private set; }
        public int TariffId { get; private set; }

        public Consumer Consumer { get; private set; }
        public Service Service { get; private set; }
        public Tariff Tariff { get; private set; }
    }
}
