using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoEnumerable
{
    public class FilterPredicate<T> : IPredicate<T>
    {
        private Predicate<T> predicate;

        public FilterPredicate(Predicate<T> predicate)
        {
            this.predicate = predicate;
        }

        public bool IsMatching(T item)
        {
            return this.predicate.Invoke(item);
        }
    }
}
