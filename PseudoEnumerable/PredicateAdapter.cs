using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return predicate.Invoke(item);
        }
    }
}
