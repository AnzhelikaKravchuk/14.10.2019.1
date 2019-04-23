using System;

namespace PseudoEnumerable.Tests
{
    public class Contain : IPredicate<int>
    {
        private int _key;

        private int Key
        {
            set
            {
                if (value < 0 || value > 9)
                {
                    throw new ArgumentOutOfRangeException($"{nameof(value)} cannot be less than 0 or bigger than 9.");
                }

                _key = value;
            }
        }


        public Contain(int key)
        {
            Key = key;
        }
        public bool IsMatching(int number)
        {
            return CheckNumber(number);
        }

        private bool CheckNumber(int number)
        {
            int[] result = ConvertToArray(number);

            return Array.IndexOf(result, _key) >= 0;
        }

        private int[] ConvertToArray(int number)
        {
            int[] result = new int[GetDigitLength(number)];

            for (int i = result.Length - 1; i >= 0; i--)
            {
                result[i] = number % 10;
                number /= 10;
            }

            return result;
        }
        private static int GetDigitLength(int number)
        {
            int count = (number == 0) ? 1 : 0;
            while (number != 0)
            {
                count++;
                number /= 10;
            }

            return count;
        }
    }
}
