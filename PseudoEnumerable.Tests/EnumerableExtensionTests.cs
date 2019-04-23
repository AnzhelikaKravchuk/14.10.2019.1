using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PseudoEnumerable.Tests
{
    public class EnumerableExtensionTests
    {
        private static object[] filterTestCases =
        {
            new object[] { new int[] { -343, 13, 55, 546877568 }, new FindDigitPredicate(3), new int[] { -343, 13 } },
            new object[] { new int[] { -343, 13, 55, 546877568 }, new FindDigitPredicate(5), new int[] { 55, 546877568 } },
        };

        [TestCaseSource("filterTestCases")]
        public static void FilterTest(int[] array, IPredicate<int> predicate, int[] expected)
        {
            CollectionAssert.AreEqual(expected, array.Filter(predicate));
        }

        private static object[] filterDelgateTestCases =
        {
            new object[] { new int[] { -343, 13, 55, 546877568 }, new FindDigitPredicate(3), new int[] { -343, 13 } },
            new object[] { new int[] { -343, 13, 55, 546877568 }, new FindDigitPredicate(5), new int[] { 55, 546877568 } },
        };

        [TestCaseSource("filterDelgateTestCases")]
        public static void FilterdelegateTest(int[] array, IPredicate<int> predicate, int[] expected)
        {
            Predicate<int> pred = predicate.IsMatching;

            CollectionAssert.AreEqual(expected, array.Filter(pred));
        }
    }
}
