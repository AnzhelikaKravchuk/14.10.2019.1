using System;
using System.Collections.Generic;
using PseudoEnumerable.Interfaces;

namespace PseudoEnumerable
{
    public static class EnumerableExtension
    {
        #region Implementation through interfaces

        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source,
            IPredicate<TSource> predicate)
        {
            // Add implementation method Filter from class ArrayExtension (Homework Day 9. 03.10.2019 Tasks 1-2)
            if (source is null)
            {
                throw new ArgumentNullException($"{nameof(source)} cannot be null.");
            }

            if (predicate is null)
            {
                throw new ArgumentNullException($"{nameof(predicate)} cannot be null.");
            }

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
            return Transform<TSource, TResult>(source, x => transformer.Transform(x));
        }

        public static IEnumerable<TSource> OrderAccordingTo<TSource>(this IEnumerable<TSource> source,
            IComparer<TSource> comparer)
        {
            // Add implementation method OrderAccordingTo from class ArrayExtension (Homework Day 9. 03.10.2019 Tasks 1-2)
            if (source is null)
            {
                throw new ArgumentNullException($"{nameof(source)} cannot be null.");
            }

            if (comparer is null)
            {
                throw new ArgumentNullException($"{nameof(comparer)} cannot be null");
            }

            var list = new List<TSource>(source);

            bool isSorted;
            do
            {
                isSorted = true;

                for (int i = 1; i < list.Count; i++)
                {
                    if (comparer.Compare(list[i], list[i - 1]) < 0)
                    {
                        var temp = list[i];
                        list[i] = list[i - 1];
                        list[i - 1] = temp;
                        isSorted = false;
                    }
                }
            }
            while (!isSorted);

            return list.ToArray();
        }

        #endregion

        #region Implementation vs delegates

        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source,
            Predicate<TSource> predicate)
        {
            // Call EnumerableExtension.Filter with interface
            return Filter<TSource>(source, new ConcretePredicate<TSource>(predicate));
        }

        public static IEnumerable<TResult> Transform<TSource, TResult>(this IEnumerable<TSource> source,
            Converter<TSource, TResult> transformer)
        {
            // Implementation logic vs delegate Converter here 
            if (source is null)
            {
                throw new ArgumentNullException($"{nameof(source)} cannot be null.");
            }

            if (transformer is null)
            {
                throw new ArgumentNullException($"{nameof(transformer)} cannot be null.");
            }

            foreach (var item in source)
            {
                yield return transformer(item);
            }
        }

        public static IEnumerable<TSource> OrderAccordingTo<TSource>(this IEnumerable<TSource> source,
            Comparison<TSource> comparer)
        {
            // Call EnumerableExtension.OrderAccordingTo with interface
            return OrderAccordingTo<TSource>(source, new ConcreteOrderingTo<TSource>(comparer));
        }

        #endregion
    }
}