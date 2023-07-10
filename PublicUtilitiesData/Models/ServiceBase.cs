using System.Collections.Generic;

namespace PublicUtilitiesData.Models
{
    public class ServiceBase
    {
        private ServiceBase() { }
        public ServiceBase(string name)
        {
            Name = name;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }

        public List<Service> Services { get; private set; } = new List<Service>();
        public List<MetersType> MetersTypes { get; private set; } = new List<MetersType>();
    }
}
