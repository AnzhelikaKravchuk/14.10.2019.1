using System;
using System.Collections.Generic;

namespace PseudoEnumerable
{
    public static class EnumerableExtension
    {
        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source,
            IPredicate<TSource> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException($"{nameof(source)} cannot be null");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException($"{nameof(predicate)} cannot be null");
            }

            foreach (TSource number in source)
            {
                if (predicate.IsMatching(number))
                {
                    yield return number;
                }
            }
        }

        public static IEnumerable<TResult> Transform<TSource, TResult>(this IEnumerable<TSource> source,
            ITransformer<TSource, TResult> transformer)
        {
            // Call EnumerableExtension.Transform with delegate
            throw new NotImplementedException();
        }

        public static IEnumerable<TSource> SortBy<TSource>(this IEnumerable<TSource> source,
            IComparer<TSource> comparer)
        {
            if (source == null)
            {
                throw new ArgumentNullException($"{nameof(source)} cannot be null");
            }

            if (comparer == null)
            {
                throw new ArgumentNullException($"{nameof(comparer)} cannot be null");
            }

            List<TSource> list = new List<TSource>(source);
            list.Sort(comparer);

            return list;
        }

        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source,
            Predicate<TSource> predicate)
        {
            // Call EnumerableExtension.Filter with interface
            throw new NotImplementedException();
        }

        public static IEnumerable<TResult> Transform<TSource, TResult>(this IEnumerable<TSource> source,
            Converter<TSource, TResult> transformer)
        {
            // Implementation Day 13 Task 1 (ArrayExtension)
            throw new NotImplementedException();
        }

        public static IEnumerable<TSource> SortBy<TSource>(this IEnumerable<TSource> source,
            Comparison<TSource> comparer)
        {
            // Call EnumerableExtension.SortBy with interface
            throw new NotImplementedException();
        }
    }
}
