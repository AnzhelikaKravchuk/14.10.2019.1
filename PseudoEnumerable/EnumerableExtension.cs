using System;
using System.Collections.Generic;

namespace PseudoEnumerable
{
    public static class EnumerableExtension
    {
        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source,
            IPredicate<TSource> predicate)
        {
            foreach (var item in source)
            {
                if (predicate.IsMatching(item))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source,
            Predicate<TSource> predicate)
        {
            PredicateDelegate<TSource> pr = new PredicateDelegate<TSource>(predicate);
            return source.Filter(pr);
        }

        public static IEnumerable<TResult> Transform<TSource, TResult>(this IEnumerable<TSource> source,
            ITransformer<TSource, TResult> transformer)
        {
            Converter<TSource, TResult> converter = transformer.Transform;
            return source.Transform(converter);
        }

        public static IEnumerable<TResult> Transform<TSource, TResult>(this IEnumerable<TSource> source,
           Converter<TSource, TResult> transformer)
        {
            foreach (var item in source)
            {
                yield return transformer(item);
            }
        }

        public static IEnumerable<TSource> SortBy<TSource>(this IEnumerable<TSource> source,
            IComparer<TSource> comparer)
        {
            var sorted = Sorted(source, comparer);
            foreach (var item in sorted)
            {
                yield return item;
            }
        }   

        public static IEnumerable<TSource> SortBy<TSource>(this IEnumerable<TSource> source,
            Comparison<TSource> comparer)
        {
            // Call EnumerableExtension.SortBy with interface
            throw new NotImplementedException();
        }

        private static IEnumerable<TSource> Sorted<TSource>(IEnumerable<TSource> collection, IComparer<TSource> comparer)
        {
            List<TSource> result = new List<TSource>(collection);
            result.Sort(comparer);
            return result;
        }
        
        class PredicateDelegate<TSource> : IPredicate<TSource>
        {
            Predicate<TSource> predicate;
            public PredicateDelegate(Predicate<TSource> predicate)
            {
                this.predicate = predicate;
            }

            public bool IsMatching(TSource item)
            {
                return predicate(item);
            }
        }
    }
}