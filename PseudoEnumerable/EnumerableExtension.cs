using System;
using System.Collections.Generic;

namespace PseudoEnumerable
{
    public static class EnumerableExtension
    {
        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source,
            IPredicate<TSource> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return FilterIterator();

            IEnumerable<TSource> FilterIterator()
            {
                foreach (var item in source)
                {
                    if (predicate.IsMatching(item))
                    {
                        yield return item;
                    }
                }
            }
        }

        public static IEnumerable<TResult> Transform<TSource, TResult>(this IEnumerable<TSource> source,
            ITransformer<TSource, TResult> transformer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (transformer is null)
            {
                throw new ArgumentNullException(nameof(transformer));
            }

            return Transform(source, transformer.Transform);
        }

        public static IEnumerable<TSource> SortBy<TSource>(this IEnumerable<TSource> source,
            IComparer<TSource> comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            TSource[] array = ToArray(source);
            Array.Sort(array, comparer);

            return array;
        }

        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source,
            Predicate<TSource> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return Filter(source, new PredicateDelegateAdapter<TSource>(predicate));
        }

        public static IEnumerable<TResult> Transform<TSource, TResult>(this IEnumerable<TSource> source,
            Converter<TSource, TResult> transformer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (transformer is null)
            {
                throw new ArgumentNullException(nameof(transformer));
            }

            return TransformIterator();

            IEnumerable<TResult> TransformIterator()
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
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            return SortBy(source, new ComparisonDelegateAdapter<TSource>(comparer));
        }

        #region Private methods

        private static T[] ToArray<T>(IEnumerable<T> source)
        {
            T[] array = new T[0];
            int count = 0;

            foreach (var item in source)
            {
                if (array.Length == 0)
                {
                    array = new T[4];
                }
                else if (array.Length == count)
                {
                    T[] tempArray = new T[count << 1];
                    Array.Copy(array, tempArray, count);
                    array = tempArray;
                }

                array[count] = item;
                count++;
            }

            T[] resultArray = new T[count];
            Array.Copy(array, resultArray, count);

            return resultArray;
        }

        #endregion
    }
}
