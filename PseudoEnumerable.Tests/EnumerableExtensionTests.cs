using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using NUnit.Framework;
using PseudoEnumerable;

namespace PseudoEnumerable.Tests
{
    [TestFixture]
    public class EnumerableExtensionTests
    {
        #region initial tests

        [Test]
        public void TestTest()
        {
            Assert.IsFalse(false);
        }

        [TestCase('9', new int[] { 309, 94, 8000, 26, -9, -119, 90, 21, 45, 6 }, ExpectedResult = new int[] { 309, 94, 8000, 26, 90, 21, 45, 6 })]
        public IEnumerable<int> Filter_FilterArrayByKey_ValidParametersDigitAsCharTest(char digit, int[] array)
        {
            return array.Filter(x => x > 0);
        }

        [TestCase(new int[] { 309, 94, 8000 }, ExpectedResult = new int[] { 4, 4, 4 })]
        public IEnumerable<int> TransformerDelegateTest(int[] array)
        {
            return array.Transform(x => 4);
        }

        [TestCase(new int[] { 309, 94, 8000 }, ExpectedResult = new int[] { 4, 4, 4 })]
        public IEnumerable<int> TransformerInterfaceTest(int[] array)
        {
            return array.Transform(x => 4);
        }

        #endregion

        #region Filter

        [Test]
        public void Filter_ArgumentNullException_NullFilterTest()
        {
            int[] array = new int[] { 6 };
            IPredicate<int> predicate = null;
            Assert.Throws<ArgumentNullException>(() => array.Filter(predicate));
        }

        [Test]
        public void Filter_ArgumentNullException_NullArrayTest()
        {
            int[] array = null;
            Assert.Throws<ArgumentNullException>(() => array.Filter(x => x % 2 == 0));
        }

        [TestCase(new int[] { 0 }, ExpectedResult = new int[] { 0 })]
        [TestCase(new int[] { -2, 0 }, ExpectedResult = new int[] { -2, 0 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 68, 69, 70, 15, 77 }, ExpectedResult = new int[] { 2, 4, 6, 68, 70 })]
        [TestCase(new int[] { 606, 11, 7, 19 }, ExpectedResult = new int[] { 606 })]
        [TestCase(new int[] { 777, 77, 77, 707, 7 }, ExpectedResult = new int[] { })]
        [TestCase(new int[] { 1, 1, 1, 1, 1, 1 }, ExpectedResult = new int[] { })]
        [TestCase(new int[] { 18, 12344321, -33233, 1234321, 1235, 2346, 0128 }, ExpectedResult = new int[] { 18, 2346, 0128 })]
        public IEnumerable<int> Filter_FilterEven_ValidParametersTest(int[] array)
            => array.Filter(x => x % 2 == 0);

        [TestCase(new int[] { 309, 94, 8000, 26, -9, -119, 90, 21, 45, 6 }, ExpectedResult = new int[] { 309, 94, 8000, 26, 90, 21, 45, 6 })]
        public IEnumerable<int> Filter_FilterArrayByKey_ValidParametersDigitAsCharTest(int[] array)
        {
            return array.Filter(x => x > 0);
        }

        [TestCase(arg: new string[] { "sdfsdf", "sf", "", "yyyy" }, ExpectedResult = new[] { "sdfsdf", "yyyy" })]
        public IEnumerable<string> Filter_StringTest(string[] array)
        {
            return array.Filter(x => x.Length > 3);
        }

        #endregion

        #region Transformer tests

        [Test]
        public void Transformer_ArgumentNullException_NullFilterTest()
        {
            var array = new double[] { 1 };
            Assert.Throws<ArgumentNullException>(() => array.Transform((Converter<double, int>)null));
        }

        [Test]
        public void Transformer_ArgumentNullException_NullArrayTest()
        {
            double[] array = null;
            Assert.Throws<ArgumentNullException>(() => array.Transform(x => -x));
        }

        [Test]
        public void Transformer_ArgumentException_EmptyArrayTest()
        {
            double[] array = null;
            Assert.Throws<ArgumentNullException>(() => array.Transform(x => x));
        }

        [TestCase(new[] { -23.809, 0.001 }, ExpectedResult = new[] { "-23.809", "0.001" })]
        public IEnumerable<string> Transform_TransformToWordsENTests(double[] array) => array.Transform(x => x.ToString(CultureInfo.InvariantCulture));

        [TestCase(arg: new[] { "dd", "ddd" }, ExpectedResult = new[] { 2, 3 })]
        public IEnumerable<int> Transform_TransformToWordsRUTests(string[] array) => array.Transform(x => x.Length);

        [TestCase(new[] { 9.809, 1.00001 }, ExpectedResult = new[] { 9, 1 })]
        public IEnumerable<int> Transform_TransformBinaryTests(double[] array) => array.Transform(x => (int)x);

        #endregion

        #region

        [Test]
        public void Sort_ArgumentNullException_NullFilterTest()
        {
            var array = new int[] { 2 };
            Assert.Throws<ArgumentNullException>(() => array.SortBy((Comparison<int>)null));
        }

        [Test]
        public void Sort_ArgumentNullException_NullArrayTest()
        {
            string[] array = null;
            Assert.Throws<ArgumentNullException>(() => array.SortBy((x, y) => x.CompareTo(y)));
        }

        [Test, TestCaseSource(typeof(TestCasesClass), nameof(TestCasesClass.TestCasesStringsAsc))]
        public IEnumerable<string> Sort_StringLengthAscTest(string[] array)
        {
            return array.SortBy((x, y) => x.Length.CompareTo(y.Length));
        }

        private class TestCasesClass
        {
            public static IEnumerable TestCasesStringsAsc
            {
                get
                {
                    yield return new TestCaseData(
                        new[] { new[] { "Когда", "моя", "музыка", "вновь", "захлебнется", "от", "крика" } })
                        .Returns(new[] { "от", "моя", "Когда", "вновь", "крика", "музыка", "захлебнется" });

                    yield return new TestCaseData(
                        new[] { new[] { "veni", "vidi", "vici" } })
                        .Returns(new[] { "veni", "vidi", "vici" });

                    yield return new TestCaseData(
                        new[] { new[] { "", "kk", "" } })
                        .Returns(new[] { "", "", "kk" });
                }
            }
        }

        #endregion

        private class SillyTransform : ITransformer<int, int>
        {
            public int Transform(int item)
            {
                return 4;
            }
        }
    }
}
