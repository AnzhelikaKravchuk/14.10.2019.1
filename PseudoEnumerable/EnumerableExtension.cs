using System;
using System.Collections.Generic;

namespace PseudoEnumerable
{
    public static class EnumerableExtension
    {
        #region Interface methods 

        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source, IPredicate<TSource> predicate)
        {
            if (source == null || predicate == null)
            {
                throw new ArgumentNullException("Wrong argument in Filter method.");
            }

            return source.FilterLazy(predicate);
        }

        public static IEnumerable<TResult> Transform<TSource, TResult>(this IEnumerable<TSource> source,
            ITransformer<TSource, TResult> transformer)
        {
            if (source == null || transformer == null)
            {
                throw new ArgumentNullException("Wrong argument in Transform method.");
            }

            return source.TransformLazy(transformer.Transform);
        }

        public static IEnumerable<TSource> SortBy<TSource>(this IEnumerable<TSource> source,
            IComparer<TSource> comparer)
        { 
            if (comparer == null || source == null)
            {
                throw new ArgumentNullException($"Wrong argument in SortBy method.");
            }

            return source.SortByLazy(comparer);
        }

        #endregion

        #region Delegate methods

        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source,
            Predicate<TSource> predicate)
        {
            if (source == null || predicate == null)
            {
                throw new ArgumentNullException("Wrong argument in Filter method.");
            }

            PredicateAdapter<TSource> adapter = new PredicateAdapter<TSource>(predicate);
            return source.FilterLazy(adapter);
        }

        public static IEnumerable<TResult> Transform<TSource, TResult>(this IEnumerable<TSource> source,
            Converter<TSource, TResult> transformer)
        {
            if (source == null || transformer == null)
            {
                throw new ArgumentNullException("Wrong argument in Transform method.");
            }

            return source.TransformLazy(transformer);
        }

        public static IEnumerable<TSource> SortBy<TSource>(this IEnumerable<TSource> source,
            Comparison<TSource> comparer)
        {
            if (comparer == null || source == null)
            {
                throw new ArgumentNullException($"Wrong argument in SortBy method.");
            }

            ComparisonAdapter<TSource> comparerAdapter = new ComparisonAdapter<TSource>(comparer);
            return source.SortByLazy(comparerAdapter);
        }

        #endregion

        #region Private Methods

        private static IEnumerable<TSource> FilterLazy<TSource>(this IEnumerable<TSource> source, IPredicate<TSource> predicate)
        {
            foreach (var element in source)
            {
                if (predicate.IsMatching(element))
                {
                    yield return element;
                }
            }
        }

        private static IEnumerable<TResult> TransformLazy<TSource, TResult>(this IEnumerable<TSource> source,
            Converter<TSource, TResult> transformer)
        {
            foreach (var element in source)
            {
                yield return transformer(element);
            }
        }

        private static IEnumerable<TSource> SortByLazy<TSource>(this IEnumerable<TSource> source, IComparer<TSource> comparer)
        {
            TSource[] sourceArray = new List<TSource>(source).ToArray();
            Array.Sort(sourceArray, comparer);
            foreach (var element in sourceArray)
            {
                yield return element;
            }
        }

        #endregion
    }
}
