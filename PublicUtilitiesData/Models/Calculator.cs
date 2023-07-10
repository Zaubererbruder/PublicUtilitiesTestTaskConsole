using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicUtilitiesData.Models
{
    public class Calculator
    {
        public Calculator() { }
        public Calculator(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}
