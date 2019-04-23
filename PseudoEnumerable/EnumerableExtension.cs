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
            FilterCheckingExceptions(source, predicate);

            foreach (var item in source)
            {
                if (predicate.IsMatching(item))
                {
                    yield return item;
                }
            }
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
            SortCheckingExceptions(source, comparer);

            int count = GetCount(source);

            var result = new TSource[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = source.ElementAt(i);
            }

            Array.Sort(result, comparer);

            return result;
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

        #region Private Methods

        private static bool FilterCheckingExceptions<T>(IEnumerable<T> array, IPredicate<T> predicate)
        {
            if (array == null)
            {
                throw new ArgumentNullException($"{nameof(array)} must be not null.");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException($"{nameof(predicate)} must be not null.");
            }

            return true;
        }

        private static bool TransformCheckingExceptions<TSource, TResult>(IEnumerable<TSource> array, ITransformer<TSource, TResult> transformer)
        {
            if (array == null)
            {
                throw new ArgumentNullException($"{nameof(array)} must be not null.");
            }

            if (transformer == null)
            {
                throw new ArgumentNullException($"{nameof(transformer)} must be not null.");
            }

            return true;
        }

        private static bool SortCheckingExceptions<T>(IEnumerable<T> array, IComparer<T> comparer)
        {
            if (array == null)
            {
                throw new ArgumentNullException($"{nameof(array)} must be not null.");
            }

            if (comparer == null)
            {
                throw new ArgumentNullException($"{nameof(comparer)} must be not null.");
            }

            return true;
        }

        private static bool SortCheckingExceptions<T>(IEnumerable<IEnumerable<T>> array, IComparer<IEnumerable<T>> comparer)
        {
            if (array == null)
            {
                throw new ArgumentNullException($"{nameof(array)} must be not null.");
            }

            if (comparer == null)
            {
                throw new ArgumentNullException($"{nameof(comparer)} must be not null.");
            }

            return true;
        }

        private static int GetCount<TSource>(IEnumerable<TSource> source)
        {
            int count = 0;
            ICollection<TSource> c = source as ICollection<TSource>;
            if (c != null)
            {
                count = c.Count;
            }

            using (IEnumerator<TSource> enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    count++;
                }
            }

            return count;
        }

        #endregion
    }
}
