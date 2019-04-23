using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PseudoEnumerable.Tests
{
    [TestFixture]
    public class EnumerableExtensionTests
    {
        [TestCase(new double[] { 0.1, 15.14 }, 1, new double[] { 0.1, 15.14 })]
        [TestCase(new double[] { -1.01, 15 }, 1, new double[] { -1.01, 15 })]
        [TestCase(new double[] { 0.021, 0 }, 0, new double[] { 0.021, 0 })]
        public void FilterArrayByDigit_DoubleValueCorrectInputArrayAndValidDigit_TheFilteredArray(double[] array, int digit, double[] expected)
        {
            var actual = array.Filter(new DigitInNumber<double>(digit));

            int i = 0;

            foreach (var item in actual)
            {
                Assert.AreEqual(expected[i++], item);
            }
        }

        [TestCase(new int[] { 2, 3, 4, 6 }, new int[] { 2, 4, 6 })]
        public void FilterArrayByDigit_PredicateDoubleValueCorrectInputArrayAndValidDigit_TheFilteredArray(int[] array, int[] expected)
        {            
            var actual = array.Filter(new Predicate<int>(FiltersNumber));

            int i = 0;

            foreach (var item in actual)
            {
                Assert.AreEqual(expected[i++], item);
            }
        }

        public bool FiltersNumber(int number)
        {
            return number % 2 == 0;
        }

        [Test]
        public void Transform()
        {
            int[] input = { 5, 10, 3, 2 };
            int[] expected = { 10, 20, 6, 4 };

            var actual = input.Transform(new Converter<int, int>(TransformToSumNumber));

            int i = 0;

            foreach (var item in actual)
            {
                Assert.AreEqual(expected[i++], item);
            }
        }

        public int TransformToSumNumber(int number)
        {
            return number + number;
        }
    }
}
