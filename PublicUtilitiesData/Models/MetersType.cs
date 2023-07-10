using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace PublicUtilitiesData.Models
{
    public class MetersType
    {
        private MetersType() { }
        public MetersType(string name, ServiceBase serviceBase)
        {
            Name = name;
            ServiceBase = serviceBase;
            ServiceBaseId = serviceBase.Id;
        }

        public int Id { get; private set; }
        public int ServiceBaseId { get; private set; }
        public string Name { get; private set; }
        public ServiceBase ServiceBase { get; private set; }

        public override string ToString()
        {
            return Name;
        }

        public static IEnumerable<MetersType> GetAll()
        {
            using(var context = new PUDbContext())
            {
                return context.MetersTypes.Include(mt => mt.ServiceBase).ToList();
            }
        }
    }
}
