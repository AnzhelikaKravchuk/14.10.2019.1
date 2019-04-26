using System;
using System.Collections.Generic;

namespace PseudoEnumerable
{
    public static class EnumerableExtension
    {
        #region Public methods
        public static IEnumerable<TSource> Filter<TSource>(
            this IEnumerable<TSource> source, IPredicate<TSource> predicate)
        {
            CheckArguments(source, predicate);

            return FilterLogic(source, predicate);
        }
        
        public static IEnumerable<TSource> Filter<TSource>(
            this IEnumerable<TSource> source, Predicate<TSource> predicate)
        {
            return source.Filter(new AdapterFilter<TSource>(predicate));
        }

        public static IEnumerable<TResult> Transform<TSource, TResult>(
            this IEnumerable<TSource> source, ITransformer<TSource, TResult> transformer)
        {
            CheckArguments(source, transformer);

            return source.Transform(transformer.Transform);
        }

        public static IEnumerable<TResult> Transform<TSource, TResult>(
            this IEnumerable<TSource> source, Converter<TSource, TResult> transformer)
        {
            CheckArguments(source, transformer);

            return TransformerLogic(source, transformer);
        }

        public static IEnumerable<TSource> SortBy<TSource>(
            this IEnumerable<TSource> source, IComparer<TSource> comparer)
        {
            CheckArguments(source, comparer);

            return SortByLogic(source, comparer);
        }

        public static IEnumerable<TSource> SortBy<TSource>(
            this IEnumerable<TSource> source, Comparison<TSource> comparer)
        {
            CheckArguments(source, comparer);

            return source.SortBy(new AdapterSortBy<TSource>(comparer));
        }
        #endregion Public methods

        #region Private methods
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

        private static IEnumerable<TResult> TransformerLogic<TSource, TResult>(IEnumerable<TSource> source, Converter<TSource, TResult> transformer)
        {
            foreach (TSource item in source)
            {
                yield return transformer(item);
            }
        }

        private static IEnumerable<TSource> SortByLogic<TSource>(IEnumerable<TSource> source, IComparer<TSource> comparer)
        {
            List<TSource> result = new List<TSource>(source);

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
        #endregion
    }

    internal class AdapterFilter<TSource> : IPredicate<TSource>
    {
        private Predicate<TSource> predicate;

        public AdapterFilter(Predicate<TSource> predicate)
        {
            this.predicate = predicate;
        }

        public bool IsMatching(TSource source)
        {
            return predicate.Invoke(source);
        }
    }

    internal class AdapterSortBy<TSource> : IComparer<TSource>
    {
        public AdapterSortBy(Comparison<TSource> comparison)
        {
            Comparison = comparison;
        }

        public Comparison<TSource> Comparison { get; set; }

        public int Compare(TSource x, TSource y)
        {
            return Comparison.Invoke(x, y);
        }
    }
}
