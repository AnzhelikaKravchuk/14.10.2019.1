using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PseudoEnumerable;

namespace PseudoEnumerable.Tests
{
    public class EnumerableExtensionTests
    {
        private static IEnumerable<TestCaseData> FilterTestCases
        {
            get
            {
                yield return new TestCaseData(arg1: new int[] { 12, 7, 10, 0, 3, -8, -9 }, arg2: new EvenNumberPredicate(),
                    arg3: new int[] { 12, 10, 0, -8 });
            }
        }

        [Test, TestCaseSource(nameof(FilterTestCases))]
        public void Filter_ConreteArrayAndPredicate_ReturnFilteredArray(int[] array, IPredicate<int> predicate, int[] expected)
        {
            CollectionAssert.AreEqual(array.Filter(predicate), expected);
        }

        [Test, TestCaseSource(nameof(FilterTestCases))]
        public void Filter_Delegate_ReturnFilteredArray(int[] array, IPredicate<int> predicate, int[] expected)
        {
            CollectionAssert.AreEqual(array.Filter(x => x % 2 == 0), expected);
        }

        public bool IfEvenNumber(int number)
        {
            return number % 2 == 0;
        }

        public class EvenNumberPredicate : IPredicate<int>
        {
            /// <summary>
            /// Check if number is even.
            /// </summary>
            /// <param name="number">Number to check.</param>
            /// <returns>Returns true if number is even.</returns>
            public bool IsMatching(int number)
            {
                return number % 2 == 0;
            }
        }
    }
}

