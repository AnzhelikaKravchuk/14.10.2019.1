using System;
using System.Collections.Generic;

namespace PseudoEnumerable
{
    public static class EnumerableExtension
    {
        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source,
            IPredicate<TSource> predicate)
        {
            Check(source, predicate);
            foreach (var item in source)
            {
                if (predicate.IsMatching(item))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<TResult> Transform<TSource, TResult>(this IEnumerable<TSource> source,
            ITransformer<TSource, TResult> transformer)
        {
            Check(source, transformer);
            foreach (var item in source)
            {
                yield return transformer.Transform(item);
            }
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

            var resultList = new List<TSource>(source);
            resultList.Sort(comparer);
            return resultList;
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

        private static void Check<TSource>(IEnumerable<TSource> source, IPredicate<TSource> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException($"{nameof(source)} cannot be null");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException($"{nameof(predicate)} cannot be null");
            }
        }

        private static void Check<TSource, TResult>(IEnumerable<TSource> source, ITransformer<TSource, TResult> transformer)
        {
            if (source == null)
            {
                throw new ArgumentNullException($"{nameof(source)} cannot be null");
            }

            if (transformer == null)
            {
                throw new ArgumentNullException($"{nameof(transformer)} cannot be null");
            }
        }
    }
}
