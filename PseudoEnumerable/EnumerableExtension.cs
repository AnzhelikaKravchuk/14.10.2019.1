using System;
using System.Collections.Generic;

namespace PseudoEnumerable
{
    /// <summary>
    /// Defines extension methods for IEnumerable generic collections
    /// </summary>
    public static class EnumerableExtension
    {
        /// <summary>
        /// Filters a sequence of values based on a predicate interface.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source</typeparam>
        /// <param name="source">An <see cref="IEnumerable{TSource}"/> to filter.</param>
        /// <param name="predicate">Contains method to test each source element for a condition</param>
        /// <returns>
        ///     An <see cref="IEnumerable{TSource}"/> that contains elements from the input
        ///     sequence that satisfy the condition.
        /// </returns>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="source"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="predicate"/> is null.</exception>
        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source,
            IPredicate<TSource> predicate)
        {
            ValidateIsNull(predicate, nameof(predicate));
            ValidateIsNull(source, nameof(source));

            IEnumerable<TSource> Iterate()
            {
                foreach (var item in source)
                {
                    if (!predicate.IsMatching(item))
                    {
                        continue;
                    }

                    yield return item;
                }
            }

            return Iterate();
        }

        /// <summary>
        /// Transforms each element of a sequence into a new form.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by transformer.</typeparam>
        /// <param name="source">A sequence of values to invoke a transform function on.</param>
        /// <param name="transformer">An interface with method to apply to each source element.</param>
        /// <returns>
        ///     An <see cref="IEnumerable{TResult}"/> whose elements are the result of
        ///     invoking the transform function on each element of source.
        /// </returns>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="source"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="transformer"/> is null.</exception>
        public static IEnumerable<TResult> Transform<TSource, TResult>(this IEnumerable<TSource> source,
            ITransformer<TSource, TResult> transformer)
        {
            ValidateIsNull(transformer, nameof(transformer));

            return source.Transform(transformer.Transform);
        }

        /// <summary>
        /// Sorts the elements of a sequence in ascending order by comparer method.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by key.</typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="comparer">An object with method to compare values.</param>
        /// <returns>
        ///     An <see cref="IEnumerable{TSource}"/> whose elements are sorted according to a key.
        /// </returns>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="source"/> is null.</exception>
        public static IEnumerable<TSource> SortBy<TSource>(this IEnumerable<TSource> source,
            IComparer<TSource> comparer)
        {
            ValidateIsNull(source, nameof(source));

            if (comparer == null)
            {
                comparer = Comparer<TSource>.Default;
            }

            var list = new List<TSource>(source);

            list.Sort(comparer);

            IEnumerable<TSource> Iterate()
            {
                foreach (var item in list)
                {
                    yield return item;
                }
            }

            return Iterate();
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate delegate.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source</typeparam>
        /// <param name="source">An <see cref="IEnumerable{TSource}"/> to filter.</param>
        /// <param name="predicate">Contains method to test each source element for a condition</param>
        /// <returns>
        ///     An <see cref="IEnumerable{TSource}"/> that contains elements from the input
        ///     sequence that satisfy the condition.
        /// </returns>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="source"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="predicate"/> is null.</exception>
        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source,
            Predicate<TSource> predicate)
        {
            ValidateIsNull(source, nameof(source));
            ValidateIsNull(predicate, nameof(predicate));

            return source.Filter(new PredicateAdapter<TSource>(predicate));
        }

        /// <summary>
        /// Transforms each element of a sequence into a new form.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by transformer.</typeparam>
        /// <param name="source">A sequence of values to invoke a transform function on.</param>
        /// <param name="transformer">A transform function to apply to each source element.</param>
        /// <returns>
        ///     An <see cref="IEnumerable{TResult}"/> whose elements are the result of
        ///     invoking the transform function on each element of source.
        /// </returns>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="source"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="transformer"/> is null.</exception>
        public static IEnumerable<TResult> Transform<TSource, TResult>(this IEnumerable<TSource> source,
            Converter<TSource, TResult> transformer)
        {
            ValidateIsNull(source, nameof(source));
            ValidateIsNull(transformer, nameof(transformer));

            IEnumerable<TResult> Iterate()
            {
                foreach (var item in source)
                {
                    yield return transformer(item);
                }
            }

            return Iterate();
        }
        
        /// <summary>
        /// Sorts the elements of a sequence in ascending order by comparer method.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by key.</typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="comparer">An object with method to compare values.</param>
        /// <returns>
        ///     An <see cref="IEnumerable{TSource}"/> whose elements are sorted according to a key.
        /// </returns>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="source"/> is null.</exception>
        public static IEnumerable<TSource> SortBy<TSource>(this IEnumerable<TSource> source,
            Comparison<TSource> comparer)
        {
            ValidateIsNull(source, nameof(source));
            ValidateIsNull(comparer, nameof(comparer));

            return source.SortBy(new ComparisonAdapter<TSource>(comparer));
        }

        /// <summary>
        /// Checks if value is null.
        /// </summary>
        /// <param name="value">The reference to check.</param>
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
