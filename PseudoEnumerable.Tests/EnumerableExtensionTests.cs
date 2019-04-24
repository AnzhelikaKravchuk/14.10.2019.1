using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PseudoEnumerable;

namespace PseudoEnumerable.Tests
{
    [TestFixture]
    public class EnumerableExtensionTests
    {
        #region Filter tests

        [Test]
        public void FilterTest_IfSourse_IsNull_Throw_ArgumentNullException()
        {
            int[] source = null;
            Predicate<int> predicate = x => x % 2 == 0;
            Assert.Throws<ArgumentNullException>(() => source.Filter(predicate));
        }

        [Test]
        public void FilterTest_IfPredicate_IsNull_Throw_ArgumentNullException()
        {
            List<string> source = new List<string>() { "one", "two ", "forty two", "zero" };
            Predicate<string> predicate = null;
            Assert.Throws<ArgumentNullException>(() => source.Filter(predicate));
        }

        [TestCase(new int[] { }, ExpectedResult = new int[0])]
        [TestCase(new int[] { 1, 2, 3, 4, 5 }, ExpectedResult = new int[] { 2, 4 })]
        [TestCase(new int[] { 1, 1, 1, 1 }, ExpectedResult = new int[0])]
        public IEnumerable<int> FilterTests_WithIntArray_IsEvenPredicate(int[] source)
        {
            return source.Filter(x => x % 2 == 0);
        }

        [TestCase(new object[0], ExpectedResult = new string[0])]
        [TestCase(new object[] { "one", "two ", "forty two", "zero" }, ExpectedResult = new string[0])]
        [TestCase(new object[] { "one", "two ", "Forty two", "zero" }, ExpectedResult = new string[] { "Forty two" })]
        public IEnumerable<string> FilterTests_WithStringList_StartsWithCapital(params string[] source)
        {
            List<string> list = new List<string>(source);
            return list.Filter(s => char.IsUpper(s[0]));
        }

        #endregion

        #region Transform tests

        [Test]
        public void TransformTest_IfSourse_IsNull_Throw_ArgumentNullException()
        {
            string[] source = null;
            Converter<string, int> transformer = s => s.Length;
            Assert.Throws<ArgumentNullException>(() => source.Transform(transformer));
        }

        [Test]
        public void TransformTest_IfTransformer_IsNull_Throw_ArgumentNullException()
        {
            List<string> source = new List<string>() { "one", "two ", "forty two", "zero" };
            Converter<string, ulong> transformer = null;
            Assert.Throws<ArgumentNullException>(() => source.Transform(transformer));
        }

        [TestCase(new object[] { "1234", "321", "1", "555" }, ExpectedResult = new int[] { 4660, 801, 1, 1365 })]
        [TestCase(new object[] { "FFFF", "FFF", "FF", "F" }, ExpectedResult = new int[] { 65535, 4095, 255, 15 })]
        [TestCase(new object[] { "abcd", "a8b", "f", "a8" }, ExpectedResult = new int[] { 43981, 2699, 15, 168 })]
        public IEnumerable<int> TransformTests_StringArrayToDecimalIntArray(params string[] source)
        {
            return source.Transform(s => int.Parse(s, System.Globalization.NumberStyles.HexNumber));
        }

        #endregion

        #region Sort tests

        [Test]
        public void SortByTest_IfSourse_IsNull_Throw_ArgumentNullException()
        {
            int[] source = null;
            Comparison<int> comparer = (x, y) => x - y;
            Assert.Throws<ArgumentNullException>(() => source.SortBy(comparer));
        }

        [Test]
        public void SortByTest_IfComparer_IsNull_Throw_ArgumentNullException()
        {
            List<int> source = new List<int>() { 1, 2, 3, 4 };
            Comparison<int> comparer = null;
            Assert.Throws<ArgumentNullException>(() => source.SortBy(comparer));
        }

        [TestCase(new object[] { "aaa", "aa", "a" }, ExpectedResult = new string[] { "a", "aa", "aaa" })]
        [TestCase(new object[] { "b", "bb", "aaa", "aa", "a" }, ExpectedResult = new string[] { "b", "a", "bb", "aa", "aaa" })]
        [TestCase(new object[] { "a", "aa", "aaa", "bb", "b" }, ExpectedResult = new string[] { "a", "b", "aa", "bb", "aaa" })]
        public static IEnumerable<string> SortTests_ByStringLength_DirectOrder(params string[] source)
        {
            return source.SortBy((x, y) => x.Length - y.Length);
        }

        [TestCase(new int[0], ExpectedResult = new int[0])]
        [TestCase(new int[] { 1, 2, 3, 4, 5}, ExpectedResult = new int[] { 5, 4, 3, 2, 1 })]
        [TestCase(new int[] { -100, 100, 1, 1, 2, -100 }, ExpectedResult = new int[] { 100, 2, 1, 1, -100, -100 })]

        public static IEnumerable<int> SortTests_Int_DescendingOrder(int[] source)
        {
            return source.SortBy((x, y) => y - x);
        }

        #endregion
    }
}
