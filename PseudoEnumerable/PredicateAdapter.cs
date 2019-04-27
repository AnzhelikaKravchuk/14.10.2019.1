using System;
using System.Collections.Generic;

namespace PseudoEnumerable
{
    internal class PredicateAdapter<T> : IPredicate<T>
    {
        private Predicate<T> predicate;

        public PredicateAdapter (Predicate<T> predicate)
        {
            this.predicate = predicate;
        }

        bool IPredicate<T>.IsMatching(T item)
        {
            return predicate(item);
        }
    }
}
