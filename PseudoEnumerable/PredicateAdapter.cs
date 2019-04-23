using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoEnumerable
{
    class PredicateAdapter<TSource> : IPredicate<TSource>
    {
        private Predicate<TSource> predicate;
        public PredicateAdapter(Predicate<TSource> predicate)
        {
            this.predicate = predicate;
        }

        public bool IsMatching(TSource item)
        {
           return predicate(item);
        }
    }
}
