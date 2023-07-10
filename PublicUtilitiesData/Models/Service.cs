using System.Collections.Generic;

namespace PublicUtilitiesData.Models
{
    public class Service
    {
        private Service() { }
        public Service(ServiceBase serviceBase, string name, Unit unit)
        {
            ServiceBaseId = serviceBase.Id;
            ServiceBase = serviceBase;
            Name = name;
            Unit = unit;
            UnitId = UnitId;
        }

        public int Id { get; private set; }
        public int ServiceBaseId { get; private set; }
        public string Name { get; private set; }
        public int UnitId { get; private set; }

        public ServiceMetersType ServiceMetersType { get; private set; }
        public MetersType MetersType => ServiceMetersType?.MetersType;
        public ServiceBase ServiceBase { get; private set; }
        public Unit Unit { get; private set; }
        public List<Tariff> Tariffs { get; private set; } = new List<Tariff>();
    }
}
