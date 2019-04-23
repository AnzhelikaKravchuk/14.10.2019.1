using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoEnumerable
{
    public class ComparisonAdapter<T> : IComparer<T>
    {
        private readonly Comparison<T> comparison;

        public ComparisonAdapter(Comparison<T> comparison)
        {
            this.comparison = comparison;
        }

        public int Compare(T x, T y)
        {
            return this.comparison.Invoke(x, y);
        }
    }
}
