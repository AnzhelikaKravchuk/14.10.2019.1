using System;
using System.Collections.Generic;
using System.Linq;

namespace PseudoEnumerable
{
    public static class EnumerableExtension
    {
        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source,
            IPredicate<TSource> predicate)
        {
            Validate(source);

            if (predicate is null)
            {
                throw new ArgumentException($"{nameof(predicate)} cannot be null.");
            }

            return FilterCollection(source, predicate);
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
            Validate(source);

            var result = new List<TSource>(source);
            result.Sort(comparer);

            return ReturnSortBy(result);
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

        private static void Validate<TSource>(IEnumerable<TSource> dataSources)
        {
            if (dataSources is null)
            {
                throw new ArgumentNullException($"{nameof(dataSources)} cannot be null.");
            }
        }

        private static IEnumerable<TSource> FilterCollection<TSource>(IEnumerable<TSource> numbers, IPredicate<TSource> predicate)
        {
            foreach (var item in numbers)
            {
                if (predicate.IsMatching(item))
                {
                    yield return item;
                }
            }
        }

        private static IEnumerable<TSource> ReturnSortBy<TSource>(IEnumerable<TSource> source)
        {
            foreach (var item in source)
            {
                yield return item;
            }
        }
    }
}
