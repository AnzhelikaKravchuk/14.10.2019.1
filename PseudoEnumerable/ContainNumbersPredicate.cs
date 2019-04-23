using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoEnumerable
{
    public class ContainNumberPredicate : IPredicate<int>
        {
            private int digit;

            /// <summary>
            /// The constructor that initializes digit for checking for a containing it in an array item.
            /// </summary>
            /// <param name="digit">The value of digit.</param>
            public ContainNumberPredicate(int digit)
            {
                Digit = digit;
            }

            private int Digit
            {
                set
                {
                    if (value >= 0 && value <= 9)
                    {
                        digit = value;
                    }
                    else
                    {
                        throw new ArgumentException($"{nameof(value)} should be a digit!");
                    }
                }
            }

            /// <summary>
            /// The condition for a number that checks for containing some digit in a this number.
            /// </summary>
            /// <param name="number">Input number</param>
            /// <returns>True, if number contains digit and false, if it is not.</returns>
            public bool IsMatching(int number)
            {
                number = Math.Abs(number);
                while (number != 0)
                {
                    if (number % 10 == digit)
                    {
                        return true;
                    }

                    number /= 10;
                }

                return false;
            }
        }
}
