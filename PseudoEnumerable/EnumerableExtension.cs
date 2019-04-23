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
            CheckForExceptions(source);
            return source.FilterCollection(predicate);
        }

        private static IEnumerable<T> FilterCollection<T>(this IEnumerable<T> collection, IPredicate<T> predicate)
        {
            foreach (T number in collection)
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
            CheckForExceptions(source);
            return source.TransformCollection(transformer);
        }

        private static IEnumerable<TResult> TransformCollection<TSource, TResult>(this IEnumerable<TSource> collection,
            ITransformer<TSource, TResult> transform)
        {
            foreach (var item in collection)
            {
                yield return transform.Transform(item);
            }
        }

        public static IEnumerable<TSource> SortBy<TSource>(this IEnumerable<TSource> source,
            IComparer<TSource> comparer)
        {
            CheckForExceptions(source);
            List<TSource> result = new List<TSource>(source);
            result.Sort(comparer);
            SortByCollection(result, comparer);
            return result;
        }

        private static IEnumerable<TSource> SortByCollection<TSource>(List<TSource> result, IComparer<TSource> comparer)
        {
            foreach (var item in result)
            {
                yield return item;
            }
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

        private static void CheckForExceptions<T>(IEnumerable<T> array)
        {
            if (array is null)
            {
                throw new ArgumentNullException($"{nameof(array)} cannot be null!");
            }

            if (array.Count() == 0)
            {
                throw new ArgumentException($"{nameof(array)} cannot be empty!");
            }
        }
    }
}
