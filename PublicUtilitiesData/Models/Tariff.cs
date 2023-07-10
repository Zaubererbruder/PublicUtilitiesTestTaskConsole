using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PublicUtilitiesData.Models
{
    public class Tariff
    {
        private Tariff() { }
        public Tariff(string name, Service service, decimal cost, decimal standart)
        {
            Name = name;
            Service = service;
            ServiceId = service.Id;
            Cost = cost;
            Standart = standart;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public int ServiceId { get; private set; }
        public decimal Cost { get; private set; }
        public decimal Standart { get; private set; }

        public virtual Service Service { get; private set; }
        public List<ConsumerTariff> SetviceTariffs { get; private set; }
    }
}
