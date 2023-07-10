using System;
using System.Linq;

namespace PublicUtilitiesData.Models
{
    public class CalculationsHistory
    {
        private CalculationsHistory() { }
        public CalculationsHistory(Consumer consumer, DateTime period, Service service, decimal summ) 
        {
            Consumer = consumer;
            ConsumerId = consumer.AccountNumber;
            Period = period;
            Service = service;
            ServiceId = service.Id;
            Summ = summ;
        }

        public int ConsumerId { get; private set; }
        public DateTime Period { get; private set; }
        public int ServiceId { get; private set; }
        public decimal Summ { get; private set; }

        public Consumer Consumer { get; private set; }
        public Service Service { get; private set; }

        public static DateTime GetNextFreePeriod(Consumer consumer)
        {
            using (var context = new PUDbContext())
            {
                if (consumer == null)
                    throw new ArgumentNullException(nameof(consumer));

                var lastPeriod = context.CalculationsHistory
                    .Where(item => item.Consumer == consumer)
                    .OrderByDescending(item => item.Period)
                    .FirstOrDefault();

                if (lastPeriod == null)
                    return DateTime.Now.StartOfMonth();

                return lastPeriod.Period.AddMonths(1).StartOfMonth();
            }
        }
    }
}
