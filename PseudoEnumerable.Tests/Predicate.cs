using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoEnumerable
{
    public class FindDigitPredicate : IPredicate<int>
    {
        private int digit;

        public FindDigitPredicate(int digitToFind)
        {
            Digit = digitToFind;
        }

        private FindDigitPredicate()
        {
        }

        public int Digit { get => digit; private set => digit = value; }

        /// <summary>Filters numbers by digit existence.</summary>
        /// <param name="number">The number.</param>
        /// <returns>True if number contains given digit, otherwise false.</returns>
        public bool IsMatching(int item)
        {
            if (item < 0)
            {
                item *= -1;
            }

            while (item > 0)
            {
                if (item % 10 == Digit)
                {
                    return true;
                }

                item /= 10;
            }

            return false;
        }        
    }

    
}
