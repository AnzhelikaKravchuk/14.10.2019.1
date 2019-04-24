using System;

namespace PseudoEnumerable
{
    public class PredicateDelegateAdapter<T> : IPredicate<T>
    {
        private readonly Predicate<T> predicate;

        public PredicateDelegateAdapter(Predicate<T> predicate)
        {
            this.predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
        }

        public bool IsMatching(T item)
        {
            return predicate(item);
        }
    }
}
