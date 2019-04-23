using System;
using System.Collections.Generic;

namespace PseudoEnumerable
{
    public static class EnumerableExtension
    {
        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source,
            IPredicate<TSource> predicate)
        {
            CheckArguments(source, predicate);

            return FilterLogic(source, predicate);
        }

        public static IEnumerable<TResult> Transform<TSource, TResult>(this IEnumerable<TSource> source,
            ITransformer<TSource, TResult> transformer)
        {
            return source.Transform<TSource, TResult>(transformer.Transform);
        }

        public static IEnumerable<TSource> SortBy<TSource>(this IEnumerable<TSource> source,
            IComparer<TSource> comparer)
        {
            int counter = 0;
            List<TSource> result = new List<TSource>(source);

            foreach (var x in source)
            {
                result[counter] = x;
                counter++;
            }

            result.Sort(comparer);

            return Iterator();

            IEnumerable<TSource> Iterator()
            {
                foreach (TSource x in result)
                {
                    yield return x;
                }
            }
            
        }

        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source,
            Predicate<TSource> predicate)
        {
            return source.Filter(new Adapter<TSource>(predicate));
        }

        public static IEnumerable<TResult> Transform<TSource, TResult>(this IEnumerable<TSource> source,
            Converter<TSource, TResult> transformer)
        {
            foreach (TSource item in source)
            {
                yield return transformer(item);
            }
        }

        public static IEnumerable<TSource> SortBy<TSource>(this IEnumerable<TSource> source,
            Comparison<TSource> comparer)
        {
            // Call EnumerableExtension.SortBy with interface
            throw new NotImplementedException();
        }

        class Adapter<TSource> : IPredicate<TSource>
        {
            private Predicate<TSource> predicate;

            public Adapter(Predicate<TSource> predicate)
            {
                this.predicate = predicate;
            }

            public bool IsMatching(TSource source)
            {
                return predicate.Invoke(source);
            }
        }


        private static IEnumerable<TSource> FilterLogic<TSource>(IEnumerable<TSource> source, IPredicate<TSource> predicate)
        {
            foreach (TSource item in source)
            {
                if (predicate.IsMatching(item))
                {
                    yield return item;
                }
            }
        }

        private static void CheckArguments<TSource>(IEnumerable<TSource> source, dynamic predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException($"{nameof(source)} must not be  null");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException($"{nameof(predicate)} must not be null");
            }
        }
    }
}
