using System;
using System.Collections.Generic;

namespace PseudoEnumerable
{ 
    public static class EnumerableExtension
    {
        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source,
            IPredicate<TSource> predicate)
        {
            Validation(source);
            return FilterCore();

            IEnumerable<TSource> FilterCore()
            {
                foreach (var number in source)
                {
                    if (predicate.IsMatching(number))
                    {
                        yield return number;
                    }
                }
            }
        }

        public static IEnumerable<TResult> Transform<TSource, TResult>(this IEnumerable<TSource> source,
            ITransformer<TSource, TResult> transformer)
        {
            Validation(source);
            return TransformCore();

            IEnumerable<TResult> TransformCore()
            {
                foreach (var element in source)
                {
                    yield return transformer.Transform(element);
                }
            }
        }

        public static IEnumerable<TSource> SortBy<TSource>(this IEnumerable<TSource> source,
            IComparer<TSource> comparer)
        {
            Validation(source);
            return SortByCore();

            IEnumerable<TSource> SortByCore()
            {
                List<TSource> list = new List<TSource>(source);
                list.Sort(comparer);
                foreach (var element in list)
                {
                    yield return element;
                }
            }
        }

        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source,
            Predicate<TSource> predicate)
        {
            Validation(source);
            return source.Filter(new PredicateAdapter<TSource>(predicate));
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

        private static void Validation<T>(IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException($"{nameof(source)} is null");
            }

            ICollection<T> collection = source as ICollection<T>;

            if (collection.Count is 0)
            {
                throw new ArgumentException($"{nameof(source)} is empty");
            }
        }

        private class PredicateAdapter<T> : IPredicate<T>
        {
            private Predicate<T> predicate;

            public PredicateAdapter(Predicate<T> predicate)
            {
                this.predicate = predicate;
            }

            public bool IsMatching(T item) => predicate.Invoke(item);

        }
    }
}
