using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PublicUtilitiesData.Models
{
    public class MetersData
    {
        private MetersData() { }

        public MetersData(DateTime period, Consumer consumer, MetersType metersType, decimal? metersValue)
        {
            Period = period;
            ConsumerId = consumer.AccountNumber;
            MetersTypeId = metersType.Id;
            MetersValue = metersValue;

            Consumer = consumer;
            MetersType = metersType;
        }

        public DateTime Period { get; private set; }

        [ForeignKey(nameof(Consumer))]
        public int ConsumerId { get; private set; }

        [ForeignKey(nameof(MetersType))]
        public int MetersTypeId { get; private set; }
        public decimal? MetersValue {get; private set; }

        public Consumer Consumer { get; private set; }
        public MetersType MetersType { get; private set; }
    }
}
