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
            if (source == null)
            {
                throw new ArgumentNullException($"The {nameof(source)} can not be NULL.");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException($"The {nameof(predicate)} cannot be NULL.");
            }

            List<TSource> list = new List<TSource>();

            return Filter();

            IEnumerable<TSource> Filter()
            {
                foreach (var value in source)
                {
                    if (predicate.IsMatching(value))
                    {
                        yield return value;
                    }
                }
            }
        }

        public static IEnumerable<TResult> Transform<TSource, TResult>(this IEnumerable<TSource> source,
            ITransformer<TSource, TResult> transformer)
        {
            return Transform(source, transformer.Transform);
        }

        public static IEnumerable<TSource> SortBy<TSource>(this IEnumerable<TSource> source,
            IComparer<TSource> comparer)
        {
            if (source == null)
            {
                throw new ArgumentNullException($"The {nameof(source)} can not be NULL.");
            }

            IComparer<TSource> _comparer = comparer ?? (typeof(IComparable<TSource>).IsAssignableFrom(typeof(TSource)) ? Comparer<TSource>.Default : null);

            if (_comparer == null)
            {
                throw new ArgumentNullException($"The {nameof(comparer)} does not exist.");
            }

            TSource[] array = source.ToArray();
            Array.Sort(array, _comparer);

            return array;
        }

        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source,
            Predicate<TSource> predicate)
        {
            return Filter(source, new FilterPredicate<TSource>(predicate));
        }

        class FilterPredicate<TSource> : IPredicate<TSource>
        {
            private Predicate<TSource> predicate;

            public FilterPredicate(Predicate<TSource> predicate) => this.predicate = predicate;

            public bool IsMatching(TSource item) => predicate(item);
        }

        public static IEnumerable<TResult> Transform<TSource, TResult>(this IEnumerable<TSource> source,
            Converter<TSource, TResult> transformer)
        {
            if (source == null)
            {
                throw new ArgumentNullException($"The {nameof(source)} can not be NULL.");
            }

            if (transformer == null)
            {
                throw new ArgumentNullException($"The {nameof(transformer)} cannot be NULL.");
            }

            return Transform();

            IEnumerable<TResult> Transform()
            {
                foreach (var item in source)
                {
                    yield return transformer(item);
                }
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
