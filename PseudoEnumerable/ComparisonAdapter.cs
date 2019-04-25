using System;
using System.Collections.Generic;

namespace PseudoEnumerable
{
    /// <summary>
    /// Provides adapter for Comparison delegate to IComparer interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ComparisonAdapter<T> : IComparer<T>
    {
        private readonly Comparison<T> _comparison;

        /// <summary>
        /// Initializes Comparison delegate
        /// </summary>
        /// <param name="comparison">Comparison delegate</param>
        public ComparisonAdapter(Comparison<T> comparison)
        {
            _comparison = comparison;
        }

        /// <summary>
        /// Compares two values
        /// </summary>
        /// <param name="x">first value</param>
        /// <param name="y">second value</param>
        /// <returns>comparison result</returns>
        public int Compare(T x, T y)
        {
            return _comparison(x, y);
        }
    }
}
