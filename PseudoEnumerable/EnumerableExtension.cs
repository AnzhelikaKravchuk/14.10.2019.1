using System;
using System.Collections.Generic;

namespace PseudoEnumerable
{
    public static class EnumerableExtension
    {
        class CustomPredicate<T> : IPredicate<T>
        {
            Predicate<T> pred;

            public CustomPredicate(Predicate<T> pred)
            {
                this.pred = pred;
            }

            public bool IsMatching(T item)
            {
                return pred.Invoke(item);
            }
        }


        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source,
            IPredicate<TSource> predicate)
        {
            // Implementation Day 13 Task 1 (ArrayExtension)
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
            return Transform(source, transformer.Transform);
        }

        public static IEnumerable<TSource> SortBy<TSource>(this IEnumerable<TSource> source,
            IComparer<TSource> comparer)
        {
            // Implementation Day 13 Task 1 (ArrayExtension)
            List<TSource> items = new List<TSource>();
            foreach (var item in source)
            {
                items.Add(item);
            }
            items.SortBy(comparer);
            foreach (var item in items)
            {
                yield return item;
            }
        }

        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source,
            Predicate<TSource> predicate)
        {
            // Call EnumerableExtension.Filter with interface
            
            return Filter(source, new CustomPredicate<TSource>(predicate));
                
        }
               
        public static IEnumerable<TResult> Transform<TSource, TResult>(this IEnumerable<TSource> source,
            Converter<TSource, TResult> transformer)
        {
            // Implementation Day 13 Task 1 (ArrayExtension)
            foreach (var item in source)
            {
                yield return transformer.Invoke(item);
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
