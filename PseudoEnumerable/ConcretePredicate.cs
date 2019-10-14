using PseudoEnumerable.Interfaces;
using System;

namespace PseudoEnumerable
{
    class ConcretePredicate<T> : IPredicate<T>
    {
        private readonly Predicate<T> predicate;

        public ConcretePredicate(Predicate<T> predicate)
        {
            this.predicate = predicate;
        }

        public bool IsMatching(T item)
        {
            return predicate(item);
        }
    }
}
