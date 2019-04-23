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
            return Transform(source, transformer.Transform);
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
            return Filter(source, new FilterDelegateAdapter<TSource>(predicate));
        }

        public static IEnumerable<TResult> Transform<TSource, TResult>(this IEnumerable<TSource> source,
            Converter<TSource, TResult> transformer)
        {
            if (source == null)
            {
                throw new ArgumentNullException($"Array can not be null. Parameter name: { nameof(source) }.");
            }

            if (!source.GetEnumerator().MoveNext())
            {
                throw new ArgumentException($"The array should contain at least 1 element. Parameter name: { nameof(source) }");
            }

            if (transformer == null)
            {
                throw new ArgumentNullException($"Predicate can not be null. Parameter name: { nameof(transformer) }.");
            }

            return GetTransformedArray(source, transformer);
        }

        public static IEnumerable<TSource> SortBy<TSource>(this IEnumerable<TSource> source,
            Comparison<TSource> comparer)
        {
            return SortBy(source, new SortByDelegateAdapter<TSource>(comparer));
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

        private static IEnumerable<TResult> GetTransformedArray<TSource, TResult>(IEnumerable<TSource> array, Converter<TSource, TResult> transformer)
        {
            foreach (var element in array)
            { 
                yield return transformer.Invoke(element);
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

        #region Adapters

        private class FilterDelegateAdapter<T> : IPredicate<T>
        {
            private readonly Predicate<T> method;

            public FilterDelegateAdapter(Predicate<T> method)
            {
                this.method = method ?? throw new ArgumentNullException(nameof(method));
            }

            public bool IsMatching(T item)
            {
                return method.Invoke(item);
            }
        }

        private class SortByDelegateAdapter<T> : IComparer<T>
        {
            private readonly Comparison<T> comparer;

            public SortByDelegateAdapter(Comparison<T> comparer)
            {
                this.comparer = comparer ?? throw new ArgumentNullException(nameof(SortByDelegateAdapter<T>.comparer));
            }

            public int Compare(T x, T y)
            {
                return comparer.Invoke(x, y);
            }
        } 

        #endregion
    }
}
