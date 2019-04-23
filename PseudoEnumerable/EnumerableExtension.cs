using System;
using System.Collections;
using System.Collections.Generic;

namespace PseudoEnumerable
{
    public static class EnumerableExtension
    {
        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source,
            IPredicate<TSource> predicate)
        {
            CheckOnNull(source);
            CheckOnNull(predicate);

            IEnumerable<TSource> GetFilteredItems()
            {
                foreach (var item in source)
                {
                    if (predicate.IsMatching(item))
                    {
                        yield return item;
                    }
                }
            }

            return GetFilteredItems();
        }

        public static IEnumerable<TResult> Transform<TSource, TResult>(this IEnumerable<TSource> source,
            ITransformer<TSource, TResult> transformer)
        {
            return source.Transform(transformer.Transform);
        }

        public static IEnumerable<TSource> SortBy<TSource>(this IEnumerable<TSource> source,
            IComparer<TSource> comparer)
        {
            CheckOnNull(source);
            CheckOnNull(comparer);
            List<TSource> list = new List<TSource>(source);

            IEnumerable<TSource> GetSorted()
            {
                list.Sort(comparer);
                return list;
            }

            return GetSorted();
        }

        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source,
            Predicate<TSource> predicate)
        {
            return Filter<TSource>(source, new PredicateAdapter<TSource>(predicate));
        }

        private class PredicateAdapter<T>: IPredicate<T>
        {
            private Predicate<T> method;

            public bool IsMatching(T item)
            {
                return method.Invoke(item);
            }

            public PredicateAdapter(Predicate<T> predicate)
            {
                this.method = predicate;
            }          
        }

        public static IEnumerable<TSource> SortBy<TSource>(this IEnumerable<TSource> source,
            Comparison<TSource> comparer)
        {
            return source.SortBy(new SortByAdapter<TSource>(comparer));
        }

        private class SortByAdapter<TSource>: IComparer<TSource>
        {
            private Comparison<TSource> comparer;

            public SortByAdapter(Comparison<TSource> comparer)
            {
                this.comparer = comparer;
            }

            public int Compare(TSource x, TSource y)
            {
               return this.comparer(x, y);
            }
        }

        public static IEnumerable<TResult> Transform<TSource, TResult>(this IEnumerable<TSource> source,
            Converter<TSource, TResult> transformer)
        {
            CheckOnNull(source);
            CheckOnNull(transformer);

            IEnumerable<TResult> GetItems()
            {
                foreach (var item in source)
                {
                    TResult value = transformer.Invoke(item);
                    yield return value;
                }
            }

            return GetItems();
        }

        private static void CheckOnNull(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException($"{nameof(obj)} can't be null.");
            }
        }
    }
}
