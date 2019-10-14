using System;
using System.Collections.Generic;

namespace PseudoEnumerable
{
    internal class ConcreteOrderingTo<TSource> : IComparer<TSource>
    {
        private readonly Comparison<TSource> comparer;

        public ConcreteOrderingTo(Comparison<TSource> comparer)
        {
            this.comparer = comparer;
        }

        public int Compare(TSource x, TSource y)
        {
            return comparer(x, y);
        }
    }
}