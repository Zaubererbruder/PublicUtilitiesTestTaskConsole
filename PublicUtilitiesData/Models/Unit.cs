using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicUtilitiesData.Models
{
    public class Unit
    {
        private Unit() { }
        public Unit(string name)
        {
            Name = name;
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Service> Services { get; set; } = new List<Service>();

        public override string ToString()
        {
            return Name;
        }
    }
}
