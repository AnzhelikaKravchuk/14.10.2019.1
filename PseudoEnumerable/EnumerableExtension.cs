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
            return source.Transform(transformer.Transform);
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
            return source.Filter(new Adapter<TSource>(predicate));
        }

        public static IEnumerable<TResult> Transform<TSource, TResult>(this IEnumerable<TSource> source,
            Converter<TSource, TResult> transformer)
        {
            Validate(source);

            return TransformCollection(source, transformer);
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

        private static IEnumerable<TResult> TransformCollection<TSource, TResult>(IEnumerable<TSource> source,
            Converter<TSource, TResult> transformer)
        {
            foreach (var item in source)
            {
                yield return transformer(item);
            }
        }

        private static IEnumerable<TSource> ReturnSortBy<TSource>(IEnumerable<TSource> source)
        {
            foreach (var item in source)
            {
                yield return item;
            }
        }

        private class Adapter<TSource> : IPredicate<TSource>
        {
            private Predicate<TSource> predicate;

            public Adapter(Predicate<TSource> predicate)
            {
                this.predicate = predicate;
            }

            public bool IsMatching(TSource item)
            {
                return predicate(item);
            }
        }
    }
}
