using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoEnumerable
{
    /// <summary>Class for transforming double number to words representation.</summary>
    public class EnglishTransformer : ITransformer<double, string>
    {
        Dictionary<char, string> Words;
        Dictionary<double, string> SpecialCases;

        /// <summary>Gets the dictionary.</summary>
        /// <returns>Dictionary for casting double to string.</returns>
        public Dictionary<char, string> GetDictionary()
        {

            return Words = new Dictionary<char, string>
            {
                { '-', "minus" },
                { '0', "zero" },
                { '1', "one" },
                { '2', "two" },
                { '3', "three" },
                { '4', "four" },
                { '5', "five" },
                { '6', "six" },
                { '7', "seven" },
                { '8', "eight" },
                { '9', "nine" },
                { '.', "point" },
                { 'E', "E" },
                { '+', "plus" },
            };
        }

        /// <summary>Gets dictionary with special cases for double</summary>
        /// <returns>Dictionary</returns>
        public Dictionary<double, string> GetSpecialCasesDictionary()
        {
            return SpecialCases = new Dictionary<double, string>
            {
                { double.NaN, "Not A Number" },
                { double.PositiveInfinity, "Positive Infinity" },
                { double.NegativeInfinity, "Negative Infinity" }
            };
        }

        /// <summary>Gets the TResult representation of double.</summary>
        /// <param name="number">The number.</param>
        /// <returns>TResult</returns>
        public string GetTResultRepresentation(double number)
        {
            return number.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>Transforms to verbal equivalent.</summary>
        /// <param name="number">The number.</param>
        /// <returns>Verbal equivalent of given number.</returns>
        public string Transform(double number)
        {
            if (SpecialCases.TryGetValue(number, out string res))
            {
                return res;
            }

            string numberToTransform = this.GetTResultRepresentation(number);

            StringBuilder result = new StringBuilder();

            foreach (var num in numberToTransform.ToString())
            {
                result.Append($" {Words[num]}");
            }

            return result.ToString().Trim();
        }
    }
}
