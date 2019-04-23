using System;
using System.Collections.Generic;

namespace PseudoEnumerable
{
    public static class EnumerableExtension
    {
        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source,
            IPredicate<TSource> predicate)
        {

            ValidateIsNull(predicate, nameof(predicate));
            ValidateIsNull(source, nameof(source));

            foreach (var item in source)
            {
                if (!predicate.IsMatching(item))
                {
                    continue;
                }

                yield return item;
            }
        }

        public static IEnumerable<TResult> Transform<TSource, TResult>(this IEnumerable<TSource> source,
            ITransformer<TSource, TResult> transformer)
        {
            ValidateIsNull(source, nameof(source));
            ValidateIsNull(transformer, nameof(transformer));

            foreach (var item in source)
            {
                yield return transformer.Transform(item);
            }
        }

        public static IEnumerable<TSource> SortBy<TSource>(this IEnumerable<TSource> source,
            IComparer<TSource> comparer)
        {
            ValidateIsNull(source, nameof(source));

            if (comparer == null)
            {
                comparer = Comparer<TSource>.Default;
            }

            var list = new List<TSource>(source);

            list.Sort();

            foreach (var item in list)
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

        /// <summary>
        /// Checks if value is null.
        /// </summary>
        /// <param name="array">The collection to check.</param>
        /// <exception cref="ArgumentNullException">Throws when collection is null.</exception>
        private static void ValidateIsNull<T>(T value, string name)
        {
            if (value == null)
            {
                throw new ArgumentNullException($"{name} cannot be null");
            }
        }
    }
}
