using PublicUtilitiesData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicUtilitiesData
{
    public class Calculation
    {
        private List<CalculationRow> _rows;

        public Calculation(Consumer consumer, in DateTime period, CommonData commonData, IEnumerable<CalculationRow> calculationRows)
        {
            Period = period;
            Consumer = consumer;
            CommonData = commonData;
            _rows = new List<CalculationRow>(calculationRows);
        }

        public DateTime Period { get; private set; }
        public Consumer Consumer { get; private set; }
        public CommonData CommonData { get; private set; }
        public IReadOnlyList<CalculationRow> Rows => _rows;
        public decimal Summ => Rows.Sum(i=>i.CalculationResult.Summ);
    }
}
