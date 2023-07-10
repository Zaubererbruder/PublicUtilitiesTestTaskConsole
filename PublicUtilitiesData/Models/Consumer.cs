using PublicUtilitiesData.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace PublicUtilitiesData.Models
{
    public class Consumer
    {
        private Consumer() { }

        public Consumer(string name)
        {
            Name = name;
        }

        [Key]
        public int AccountNumber { get; private set; }
        public string Name { get; private set; }

        public static Consumer GetInstance(int accNumber)
        {
            using (var context = new PUDbContext())
            {
                var consumer = context.Consumers
                    .Find(accNumber);

                if (consumer == null)
                    throw new DataRowNotFoundException(nameof(context.Consumers));

                return consumer;
            }
        }
    }
}
