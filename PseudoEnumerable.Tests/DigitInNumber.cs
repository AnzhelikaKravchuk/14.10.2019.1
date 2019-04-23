using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PseudoEnumerable;

namespace PseudoEnumerable.Tests
{
    class DigitInNumber<T> : IPredicate<T>
    {
        private readonly char _digit;

        /// <summary>
        /// Initializes a new instance of the <see cref="DigitInNumber{TSource}"/> class.
        /// </summary>
        /// <param name="digit">The digit.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public DigitInNumber(int digit)
        {
            if (digit < 0 || digit > 9)
            {
                throw new ArgumentOutOfRangeException($"The {nameof(digit)} does not belong to the range from 0 to 9");
            }

            _digit = Convert.ToChar(digit.ToString());
        }

        public bool IsMatching(T item)
        {
            char[] symbols = item.ToString().ToCharArray();

            for (int i = 0; i < symbols.Length; i++)
            {
                if (symbols[i] == _digit)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
