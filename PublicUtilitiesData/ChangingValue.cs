using System;
using System.Collections.Generic;
using System.Text;

namespace PublicUtilitiesData
{
    public class ChangingValue
    {
        private List<Changing> _changes = new List<Changing>();
        private ChangingValue(List<Changing> changes)
        {
            _changes = changes;
        }

        public DateTime Period => Changes[0].Period;
        public DateTime EndOfPeriod => Period.AddMonths(1);
        public IReadOnlyList<Changing> Changes => _changes;

        public decimal Value
        {
            get
            {
                if(Changes.Count == 1) 
                    return Changes[0].Value;

                decimal res = 0;
                for (int i = 0; i < Changes.Count; i++)
                {
                    DateTime after = EndOfPeriod;
                    if(i + 1 != Changes.Count)
                        after = Changes[i + 1].Period;

                    DateTime before = Changes[i].Period;
                    TimeSpan diff = after - before;
                    int daysinMonth = DateTime.DaysInMonth(Period.Year, Period.Month) ;
                    decimal k = (decimal)diff.Days / daysinMonth;
                    res += k * Changes[i].Value;
                }
                return res;
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach(var change in Changes)
            {
                if (builder.Length > 0)
                    builder.Append(", ");

                builder.Append($"{change.Period.Day} - {change.Value}");
            }
            return builder.ToString();
        }

        public static ChangingValueBuilder Create(DateTime period, decimal startValue)
        {
            return new ChangingValueBuilder(period, startValue);
        }

        public struct Changing
        {
            internal Changing(DateTime period, decimal value)
            {
                Period = period;
                Value = value;
            }

            public DateTime Period { get; private set; }
            public decimal Value { get; private set; }
        }

        public class ChangingValueBuilder
        {
            private List<Changing> _changes = new List<Changing>();
            internal ChangingValueBuilder(DateTime period, decimal startValue)
            {
                _changes.Add(new Changing(period.StartOfMonth(), startValue));
            }

            public DateTime LastDate => _changes[_changes.Count - 1].Period;
            public decimal LastValue => _changes[_changes.Count - 1].Value;

            public ChangingValueBuilder WithChange(DateTime date, decimal newValue)
            {
                if (newValue == _changes[_changes.Count - 1].Value)
                    throw new ArgumentException("Value must be different from the last one", nameof(newValue));

                DateTime dateNewDay = date.StartOfDay();

                if(dateNewDay.Month != _changes[0].Period.Month || dateNewDay.Year != _changes[0].Period.Year)
                    throw new ArgumentException("Change must be in same month with period", nameof(date));

                if(_changes.Count != 0 && dateNewDay <= _changes[_changes.Count-1].Period)
                    throw new ArgumentException("Invalid date", nameof(date));

                _changes.Add(new Changing(dateNewDay, newValue));
                return this;
            }

            public ChangingValue Build()
            {
                return new ChangingValue(_changes);
            }
        }
    }
}
