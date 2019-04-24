using System;
using System.Collections.Generic;

namespace PseudoEnumerable
{
    public class ComparisonDelegateAdapter<T> : IComparer<T>
    {
        private readonly Comparison<T> comparer;

        public ComparisonDelegateAdapter(Comparison<T> comparer)
        {
            this.comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
        }

        public int Compare(T x, T y)
        {
            return comparer(x, y);
        }
    }
}
