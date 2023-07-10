using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicUtilitiesData.Models
{
    public class ServiceMetersType
    {
        private ServiceMetersType() { }

        public ServiceMetersType(Service service, MetersType metersType)
        {
            Service = service;
            ServiceId = service.Id;
            MetersType = metersType;
            MetersTypeId = metersType.Id;
        }

        public int ServiceId { get; private set; }
        public int MetersTypeId { get; private set; }

        public Service Service { get; private set; }
        public MetersType MetersType { get; private set; }
    }
}
