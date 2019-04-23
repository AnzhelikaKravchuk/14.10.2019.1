using System;

namespace PseudoEnumerable
{
    class DefaultPredicate<TSource> : IPredicate<TSource>
    {
        private Predicate<TSource> _predicate;

        public DefaultPredicate(Predicate<TSource> predicate)
        {
            _predicate = predicate;
        }

        public bool IsMatching(TSource item)
        {
            return _predicate(item);
        }
    }
}
