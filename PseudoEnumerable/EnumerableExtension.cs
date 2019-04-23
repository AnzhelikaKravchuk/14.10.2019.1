using System;
using System.Collections.Generic;

namespace PseudoEnumerable
{
    public static class EnumerableExtension
    {
        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source,
            IPredicate<TSource> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException($"Source can not be null. Parameter name: { nameof(source) }.");
            }

            if (!source.GetEnumerator().MoveNext())
            {
                throw new ArgumentException($"The source should contain at least 1 element. Parameter name: { nameof(source) }");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException($"Predicate can not be null. Parameter name: { nameof(predicate) }.");
            }

            return GetFilteredArray(source, predicate);
        }

        public static IEnumerable<TResult> Transform<TSource, TResult>(this IEnumerable<TSource> source,
            ITransformer<TSource, TResult> transformer)
        {
            // Call EnumerableExtension.Transform with delegate
            throw new NotImplementedException();
        }

        public static IEnumerable<TSource> SortBy<TSource>(this IEnumerable<TSource> source,
            IComparer<TSource> comparer)
        {
            if (comparer == null)
            {
                throw new ArgumentNullException($"Comparer can not be null. Parameter name: { nameof(comparer) }.");
            }

            if (source == null)
            {
                throw new ArgumentNullException($"Array can not be null. Parameter name: { nameof(source) }.");
            }

            if (!source.GetEnumerator().MoveNext())
            {
                throw new ArgumentException($"The array should contain at least 1 element. Parameter name: { nameof(source) }");
            }

            List<TSource> sortedList = new List<TSource>(source);
            sortedList.Sort(comparer);
            return sortedList;
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

        #region private methods

        private static IEnumerable<TSource> GetFilteredArray<TSource>(IEnumerable<TSource> array, IPredicate<TSource> filter)
        {
            foreach (var elemnt in array)
            {
                if (filter.IsMatching(elemnt))
                {
                    yield return elemnt;
                }
            }
        }

        private static IEnumerable<TResult> GetTransformedArray<TSource, TResult>(IEnumerable<TSource> array, ITransformer<TSource, TResult> transformer)
        {
            foreach (var element in array)
            {
                yield return transformer.Transform(element);
            }
        }

       private static void CheckInput<T>(T[] sortedArray)
       {
            if (sortedArray == null)
            {
                throw new ArgumentNullException($"Array can not be null. { nameof(sortedArray) }.");
            }

            if (sortedArray.Length == 0)
            {
                throw new ArgumentException($"Array can not be empty. { nameof(sortedArray) }.");
            }
       }

        #endregion
    }
}
