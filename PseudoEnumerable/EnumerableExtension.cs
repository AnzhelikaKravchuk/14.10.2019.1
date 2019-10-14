using System;
using System.Collections.Generic;
using System.Linq;

using PseudoEnumerable.Interfaces;

// ReSharper disable StyleCop.SA1402
namespace PseudoEnumerable
{
    public static class EnumerableExtension
    {
        #region Implementation through interfaces

        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source, IPredicate<TSource> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source), "Array can't be null");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            foreach (var i in source)
            {
                if (predicate.IsMatching(i))
                {
                    yield return i;
                }
            }
        }

        public static IEnumerable<TResult> Transform<TSource, TResult>(this IEnumerable<TSource> source, ITransformer<TSource, TResult> transformer)
        {
            // Call EnumerableExtension.Transform with delegate
            return source.Transform(transformer.Transform);
        }

        public static IEnumerable<TSource> OrderAccordingTo<TSource>(this IEnumerable<TSource> source, IComparer<TSource> comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            return Sort(source, comparer);
        }

        #endregion

        #region Implementation vs delegates

        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source, Predicate<TSource> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source), "Array can't be null");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return source.Filter(new PredicateAdapter<TSource>(predicate));
        }

        public static IEnumerable<TResult> Transform<TSource, TResult>(this IEnumerable<TSource> source, Converter<TSource, TResult> transformer)
        {
            // Implementation logic vs delegate Converter here 
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source), "Array can't be null");
            }

            if (transformer == null)
            {
                throw new ArgumentNullException(nameof(transformer));
            }

            foreach (TSource item in source)
            {
                yield return transformer(item);
            }
        }

        public static IEnumerable<TSource> OrderAccordingTo<TSource>(this IEnumerable<TSource> source, Comparison<TSource> comparer)
        {
            return source.OrderAccordingTo(Comparer<TSource>.Create(comparer));
        }

        #endregion

        private static T[] Sort<T>(IEnumerable<T> array, IComparer<T> comparer)
        {
            bool flag = true;
            var enumerable = array as T[] ?? array.ToArray();
            while (flag)
            {
                flag = false;
                for (int j = 0; j < enumerable.ToList().Count - 1; j++)
                {
                    if (comparer.Compare(enumerable[j], enumerable[j + 1]) > 0)
                    {
                        Swap(ref enumerable[j], ref enumerable[j + 1]);
                        flag = true;
                    }
                }
            }

            return enumerable;
        }

        private static void Swap<T>(ref T lhs, ref T rhs)
        {
            T tmpParam = lhs;
            lhs = rhs;
            rhs = tmpParam;
        }
    }

    internal class PredicateAdapter<TSource> : IPredicate<TSource>
    {
        private readonly Predicate<TSource> predicate;

        public PredicateAdapter(Predicate<TSource> predicate)
        {
            this.predicate = predicate;
        }

        public bool IsMatching(TSource item)
        {
            return this.predicate(item);
        }
    }
}