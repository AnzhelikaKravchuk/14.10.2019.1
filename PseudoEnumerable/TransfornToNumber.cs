using System;
using System.Text;
using System.Threading.Tasks;

namespace PseudoEnumerable
{
    public class TransformToNumber : ITransformer<string, int>
    {
        private int numberSystem;

        /// <summary>
        /// The constructor that initializes numberSystem for checking for a containing it in an array item.
        /// </summary>
        /// <param name="digit">The value of numberSystem.</param>
        public TransformToNumber(int system)
        {
            NumberSystem = system;
        }

        private int NumberSystem
        {
            set
            {
                if (value >= 2 && value <= 16)
                {
                    numberSystem = value;
                }
                else
                {
                    throw new ArgumentException($"{nameof(value)} should be in a range from 2 to 16!");
                }
            }
        }

        /// <summary>
        /// The method that transforms string to int according given number system.
        /// </summary>
        /// <param name="number">Given number in string format.</param>
        /// <returns>Number in int format.</returns>
        public int Transform(string number)
        {
            if (number is null)
            {
                throw new ArgumentNullException($"{nameof(number)} cannot be null!");
            }

            if (number == String.Empty)
            {
                throw new ArgumentException($"{nameof(number)} cannot be empty!");
            }

            string dictionary = "0123456789ABCDEF";
            var comparison = StringComparison.InvariantCultureIgnoreCase;
            for (int i = 0; i < number.Length; i++)
            {
                //if (!dictionary.Substring(0, numberSystem).Contains(number[i], comparison))
                //{
                //    throw new FormatException($"Invalid form of {nameof(number)}!");
                //}
            }

            int result = 0;
            int power = number.Length - 1;

            for (int i = 0; i < number.Length; i++)
            {

               // result += (dictionary.IndexOf(number[i], comparison)) * (int)Math.Pow(numberSystem, power);
                --power;
            }

            return result;
        }
    }
}
