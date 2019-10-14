using System;

using PseudoEnumerable.Interfaces;


namespace TrainingExtensions.Tests.PredicatesImplementations
{
    /// <summary>Checks if number contains a specified digit in it</summary>
    /// <seealso cref="IPredicate{T}" />
    public class ContainsDigit : IPredicate<int>
    {
        private readonly int digit;

        /// <summary>Initializes a new instance of the <see cref="ContainsDigit" /> class.</summary>
        /// <param name="digit">The digit.</param>
        public ContainsDigit(int digit)
        {
            this.digit = digit;
        }

        /// <summary>Predicate function.</summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     <see cref="bool" />
        /// </returns>
        public bool IsMatching(int value)
        {
            return IsContaintsDigit(value, this.digit);
        }

        private static bool IsContaintsDigit(int number, int digit)
        {
            while (number != 0)
            {
                if (Math.Abs(number % 10) == digit)
                {
                    return true;
                }

                number /= 10;
            }

            return false;
        }
    }
}