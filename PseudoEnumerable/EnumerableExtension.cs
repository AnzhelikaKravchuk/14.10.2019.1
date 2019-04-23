using System;
using System.Collections.Generic;

namespace PseudoEnumerable
{
    public static class EnumerableExtension
    {
        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source,
            IPredicate<TSource> predicate)
        {
            // Implementation Day 13 Task 1 (ArrayExtension)
            foreach (var item in source)
            {
                if (predicate.IsMatching(item))
                {
                    yield return item;
                }
            }
        }


        public static IEnumerable<TResult> Transform<TSource, TResult>(this IEnumerable<TSource> source,
            Converter<TSource, TResult> transformer)
        {
            // Implementation Day 13 Task 1 (ArrayExtension)
            foreach (var item in source)
            {
                yield return transformer(item);
            }
        }

        public static IEnumerable<TSource> SortBy<TSource>(this IEnumerable<TSource> source,
            IComparer<TSource> comparer)
        {
            // Implementation Day 13 Task 1 (ArrayExtension)
            TSource[] copiedArray = new List<TSource>(source).ToArray();

            Array.Sort(copiedArray, comparer);

            foreach (var item in copiedArray)
            {
                yield return item;
            }
        }

        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source,
            Predicate<TSource> predicate)
        {
            // Call EnumerableExtension.Filter with interface
            AdarterFilter<TSource> adarter = new AdarterFilter<TSource>(predicate);
            return source.Filter(adarter);
        }

        public class AdarterFilter<TSource> : IPredicate<TSource>
        {
            private Predicate<TSource> predicate;

            public AdarterFilter(Predicate<TSource> predicate)
            {
                this.predicate = predicate;
            }

            public bool IsMatching(TSource item) => predicate(item);
        }

        public static IEnumerable<TResult> Transform<TSource, TResult>(this IEnumerable<TSource> source,
            ITransformer<TSource, TResult> transformer)
        {
            // Call EnumerableExtension.Transform with delegate
            Converter<TSource, TResult> transformerDelegate = transformer.Transform;
            return source.Transform(transformerDelegate);
        }

        public static IEnumerable<TSource> SortBy<TSource>(this IEnumerable<TSource> source,
            Comparison<TSource> comparer)
        {
            // Call EnumerableExtension.SortBy with interface
            throw new NotImplementedException();
        }
    }
}
