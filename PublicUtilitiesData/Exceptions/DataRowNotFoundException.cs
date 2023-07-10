using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicUtilitiesData.Exceptions
{
    public class DataRowNotFoundException : Exception
    {
        public DataRowNotFoundException(string tablename) : base($"Not found a data row in table {tablename}") { }
    }
}
