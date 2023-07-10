using System;
using System.Linq;

namespace PublicUtilitiesData.Models
{
    public class PeopleCountHistory
    {
        private PeopleCountHistory() { }

        public PeopleCountHistory(Consumer consumer, DateTime changeDate, int peopleCount)
        {
            Consumer = consumer;
            ConsumerId = consumer.AccountNumber;
            ChangeDate = changeDate;
            PeopleCount = peopleCount;
        }

        public int ConsumerId { get; private set; }
        public DateTime ChangeDate { get; private set; }
        public int PeopleCount { get; private set;}

        public Consumer Consumer { get; private set; }

        public static int GetPeopleCountOnStartOfPeriod(Consumer consumer, DateTime period)
        {
            using (var context = new PUDbContext())
            {
                return (context.PeopleCountHistory
                    .Where(i => i.ChangeDate < period)
                    .OrderByDescending(i => i.ChangeDate)
                    .FirstOrDefault())?.PeopleCount ?? 1;
            }
        }
    }
}
