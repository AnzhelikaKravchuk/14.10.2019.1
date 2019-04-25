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
                throw new ArgumentNullException($"{nameof(source)} cannot be null");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException($"{nameof(predicate)} cannot be null");
            }

            foreach (TSource number in source)
            {
                if (predicate.IsMatching(number))
                {
                    yield return number;
                }
            }
        }

        public static IEnumerable<TResult> Transform<TSource, TResult>(this IEnumerable<TSource> source,
            ITransformer<TSource, TResult> transformer)
        {
            if (source == null)
            {
                throw new ArgumentNullException($"{nameof(source)} cannot be null");
            }

            if (transformer == null)
            {
                throw new ArgumentNullException($"{nameof(transformer)} cannot be null");
            }

            foreach (var item in source)
            {
                yield return transformer.Transform(item);
            }
            //return Transform<TSource, TResult>(source, transformer.Transform);
        }

        public static IEnumerable<TSource> SortBy<TSource>(this IEnumerable<TSource> source,
            IComparer<TSource> comparer)
        {
            if (source == null)
            {
                throw new ArgumentNullException($"{nameof(source)} cannot be null");
            }

            if (comparer == null)
            {
                throw new ArgumentNullException($"{nameof(comparer)} cannot be null");
            }

            List<TSource> list = new List<TSource>(source);
            list.Sort(comparer);

            return list;
        }

        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source,
            Predicate<TSource> predicate)
        {

            CheckForExceptionsPredicate(source,predicate);

            //foreach (var item in source)
            //{
            //    if (predicate(item))
            //    {
            //        yield return item;
            //    }
            //}
            
            return source.Filter(new AdapterForFilter<TSource>(predicate));       
        }

        public static IEnumerable<TResult> Transform<TSource, TResult>(this IEnumerable<TSource> source,
            Converter<TSource, TResult> transformer)
        {
            CheckForExceptionsConverter(source, transformer);

            //foreach (var item in source)
            //{
            //    yield return transformer(item);
            //}
            
           return source.Transform(new AdapterForTransform<TSource,TResult>(transformer));
        }

        public static IEnumerable<TSource> SortBy<TSource>(this IEnumerable<TSource> source,
            Comparison<TSource> comparer)
        {
            CheckForExceptionsComparison(source, comparer);
            //List<TSource> sourceList = new List<TSource>(source);
            //foreach (var item in source)
            //{
            //    sourceList.Sort(comparer);
            //}

            //return sourceList;
            return source.SortBy(new AdapterForSortBy<TSource>(comparer));
        }

        private static void CheckForExceptionsComparison<TSource>(IEnumerable<TSource> source,
            Comparison<TSource> comparer)
        {
            if (source == null)
            {
                throw new ArgumentNullException($"{nameof(source)} can't b null");
            }

            if (comparer == null)
            {
                throw new ArgumentNullException($"{nameof(comparer)} can't b null");
            }
        }

        private static void CheckForExceptionsPredicate<TSource>(IEnumerable<TSource> source,
          Predicate<TSource> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException($"{nameof(source)} can't b null");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException($"{nameof(predicate)} can't b null");
            }
        }

        private static void CheckForExceptionsConverter<TSource,TResult>(IEnumerable<TSource> source,
         Converter<TSource,TResult> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException($"{nameof(source)} can't b null");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException($"{nameof(predicate)} can't b null");
            }
        }
    }
    class AdapterForFilter<T> : IPredicate<T>
    {
        Predicate<T> Predicate { get; set; }
        public AdapterForFilter(Predicate<T> predicate)
        {
            Predicate = predicate;
        }

        public bool IsMatching(T item)
        {
            return Predicate(item);
        }
    }

    class AdapterForTransform<T1, T2> : ITransformer<T1, T2>
    {
        Converter<T1, T2> Converter { get; set; }
        public AdapterForTransform(Converter<T1, T2> convert)
        {
            Converter = convert;
        }

        public T2 Transform(T1 item)
        {
            return Converter(item);
        }
    }

    class AdapterForSortBy<T> : IComparer<T>
    {
        Comparison<T> Comparison { get; set; }

        public AdapterForSortBy(Comparison<T> comparison)
        {
            Comparison = comparison;
        }

        public int Compare(T x, T y)
        {
            return Comparison(x, y);
        }
    }
}
