using PseudoEnumerable.Interfaces;
using System;

namespace PseudoEnumerable
{
    class ConcretePredicate<TSource> : IPredicate<TSource>
    {
        private readonly Predicate<TSource> predicate;

        public ConcretePredicate(Predicate<TSource> predicate)
        {
            this.predicate = predicate;
        }

        public bool IsMatching(TSource item)
        {
            return predicate(item);
        }
    }
}
