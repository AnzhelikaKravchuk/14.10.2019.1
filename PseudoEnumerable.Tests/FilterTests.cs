using System;
using System.Collections.Generic;
using NUnit.Framework;
using PseudoEnumerable;

namespace PseudoEnumerable.Tests
{
    /// <summary>
    /// Class for Filter Method
    /// </summary>
    [TestFixture]
    public class FilterTests
    {
        [TestCase(null)]
        public void Filter_InputArrayNull_ResultArgumentNullException(string[] source) =>
            Assert.Throws<ArgumentNullException>(() => source.Filter((x) => x.Length > 0));

        [TestCase(new int[] { 1, 2 }, null)]
        public void Filter_InputPredicateNull_ResultArgumentNullException(int[] source, Predicate<int> predicate) =>
            Assert.Throws<ArgumentNullException>(() => source.Filter(predicate));

        [TestCase(new int[] { 10, 8, 3, 0 })]
        public void Filter_IntegerNumbers_LessThanZero(int[] source)
        {
            IEnumerable<int> expected = new int[] { };
            CollectionAssert.AreEqual(expected, source.Filter((x) => x < 0));
        }

        [TestCase(new char[] { '1', '8', '3', '0', 'r' })]
        public void Filter_CharNumbersAndLetters_OnlyDigits(char[] source)
        {
            IEnumerable<char> expected = new char[] { '1', '8', '3', '0' };
            CollectionAssert.AreEqual(expected, source.Filter((x) => char.IsDigit(x)));
        }
    }
}
