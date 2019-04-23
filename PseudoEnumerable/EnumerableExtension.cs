using System;
using System.Collections.Generic;

namespace PseudoEnumerable
{
    public static class EnumerableExtension
    {
        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source,
            IPredicate<TSource> predicate)
        {          
            foreach (var i in source)
            {
                if (predicate.IsMatching(i))
                {
                    yield return i;
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
            IList<TSource> copyArray = source as IList<TSource>;
            
            foreach (var i in copyArray)
            {
                for (int j = 0; j < copyArray.Count - 1; j++)
                {
                    if (comparer.Compare(copyArray[j], copyArray[j + 1]) > 0)
                    {
                        TSource str = copyArray[j];
                        copyArray[j] = copyArray[j + 1];
                        copyArray[j + 1] = str;
                    }
                }
            }

            return copyArray;
        }

        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source,
            Predicate<TSource> predicate)
        {
            return Filter(source, new Filter<TSource>(predicate));
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


    }

    public class Filter<T> : IPredicate<T>
    {
        private Predicate<T> predicate;
        public Filter(Predicate<T> predicate)
        {
            this.predicate = predicate;   
        }

        public bool IsMatching(T item)
        {
            return predicate.Invoke(item);
        }
    }
}
