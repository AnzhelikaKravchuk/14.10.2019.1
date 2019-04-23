using System;
using System.Collections.Generic;

namespace PseudoEnumerable
{
    public static class EnumerableExtension
    {
        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source,
            IPredicate<TSource> predicate)
        {
            foreach (var element in source)
            {
                if (predicate.IsMatching(element))
                {
                    yield return element;
                }
            }
        }

        public static IEnumerable<TResult> Transform<TSource, TResult>(this IEnumerable<TSource> source,
            ITransformer<TSource, TResult> transformer)
        {
            return source.Transform(transformer.Transform);
        }

        public static IEnumerable<TSource> SortBy<TSource>(this IEnumerable<TSource> source,
            IComparer<TSource> comparer)
        {
            List<TSource> input = new List<TSource>(source);
            input.Sort(comparer);
            return input;
        }

        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source,
            Predicate<TSource> predicate)
        {
            PredicateAdapter<TSource> adapter = new PredicateAdapter<TSource>(predicate);
            return source.Filter(adapter);
        }

        public static IEnumerable<TResult> Transform<TSource, TResult>(this IEnumerable<TSource> source,
            Converter<TSource, TResult> transformer)
        {
            foreach (var element in source)
            {
                yield return transformer(element);
            }
        }

        public static IEnumerable<TSource> SortBy<TSource>(this IEnumerable<TSource> source,
            Comparison<TSource> comparer)
        {
            // Call EnumerableExtension.SortBy with interface
            throw new NotImplementedException();
        }
    }
}
