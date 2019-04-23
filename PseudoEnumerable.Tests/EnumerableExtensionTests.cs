using System;
using System.Collections.Generic;
using NUnit.Framework;
using PseudoEnumerable.Tests.Comparers;

namespace PseudoEnumerable.Tests
{
    [TestFixture]
    public class EnumerableExtensionTests
    {
        #region Filter tests

        [Test]
        public void FilterTest_EvenNumberFilter()
        {
            IEnumerable<int> actualArray = new int[] { 2, -10, 13, 55, -33, 22 };
            IEnumerable<int> expectedArray = new int[] { 2, -10, 22 };

            Assert.AreEqual(expectedArray, actualArray.Filter(new EvenNumberPredicate()));
        }

        [Test]
        public void FilterTest_EvenNumberFilterPredicateDelegate()
        {
            IEnumerable<int> actualArray = new int[] { 2, -10, 13, 55, -33, 22 };
            IEnumerable<int> expectedArray = new int[] { 2, -10, 22 };

            Assert.AreEqual(expectedArray, actualArray.Filter(i => i % 2 == 0));
        }

        [Test]
        public void FilterTests_StringLengthFilterPredicateDelegate()
        {
            IEnumerable<string> actualArray = new string[] { "1234", "333", "123", "1", string.Empty };
            IEnumerable<string> expectedArray = new string[] { "333", "123" };

            Assert.AreEqual(expectedArray, actualArray.Filter(str => str.Length == 3));
        }

        #endregion

        #region Transform tests

        [Test]
        public void TransformTest_ConvertIntToString()
        {
            IEnumerable<int> actualArray = new[] { 2, 3, 4 };
            IEnumerable<string> expectedArray = new string[] { "2", "3", "4"};

            CollectionAssert.AreEqual(expectedArray, actualArray.Transform(x => x.ToString()));
        }

        [Test]
        public void TransformTest_MultiplyBy2Delegate()
        {
            IEnumerable<int> actualArray = new[] { 2, 3, 4 };
            IEnumerable<int> expectedArray = new int[] { 4, 6, 8 };

            Assert.AreEqual(expectedArray, actualArray.Transform(x => x * 2));
        }

        #endregion

        #region SortBy tests

        [Test]
        public void SortByTest_StringArrayByLength()
        {
            IEnumerable<string> actualArray = new[] { "12", "1", "1234", "123", string.Empty };
            IEnumerable<string> expectedArray = new[] { "1234", "123", "12", "1", string.Empty };

            Assert.AreEqual(expectedArray, actualArray.SortBy((x, y) => y.Length - x.Length));
        }

        [Test]
        public void SortByTest_InegerArrayAscending()
        {
            IEnumerable<int> actualArray = new int[] { 2, -10, 13, 55, -33, 22 };
            IEnumerable<int> expectedArray = new int[] { -33, -10, 2, 13, 22, 55 };

            Assert.AreEqual(expectedArray, actualArray.SortBy((x, y) => x - y));
        }

        #endregion
    }
}
