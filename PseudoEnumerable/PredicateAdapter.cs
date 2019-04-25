using System;

namespace PseudoEnumerable
{
    /// <summary>
    /// Provides adapter for Predicate delegate to IPredicate interface
    /// </summary>
    /// <typeparam name="TSource">predicate value type</typeparam>
    class PredicateAdapter<TSource> : IPredicate<TSource>
    {
        private readonly Predicate<TSource> _predicate;

        /// <summary>
        /// Initializes Predicate delegate
        /// </summary>
        /// <param name="predicate">delegate</param>
        public PredicateAdapter(Predicate<TSource> predicate)
        {
            _predicate = predicate;
        }

        /// <summary>
        /// Checks if the condition is matching
        /// </summary>
        /// <param name="item">item to check</param>
        /// <returns>matching result</returns>
        public bool IsMatching(TSource item)
        {
            return _predicate(item);
        }
    }
}
