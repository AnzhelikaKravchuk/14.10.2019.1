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
        #region Filter

        private static IEnumerable<TestCaseData> Data_For_Filter
        {
            get
            {
                #region EvenNumber

                yield return new TestCaseData(new int[] { 7, 1, 2, 3, 4, 5, 6, 7, 68, 69, 70, 15, 17 }, new EvenNumber(), new int[] { 2, 4, 6, 68, 70 });
                yield return new TestCaseData(new int[] { 0, 0, 0, 0 }, new EvenNumber(), new int[] { 0, 0, 0, 0 });
                yield return new TestCaseData(new int[] { 1, 122, 188, 199, 10001 }, new EvenNumber(), new int[] { 122, 188 });
                yield return new TestCaseData(new int[] { 1, 2, 3, 4, 6, 7, 8, 9, 34, 87, 67, 98, 17, 32 }, new EvenNumber(), new int[] { 2, 4, 6, 8, 34, 98, 32 });
                yield return new TestCaseData(new int[] { -4, -5, -7, -6, -17, -1, -5, -15, -11, -12, -6, -7, -17, -7, -14 }, new EvenNumber(), new int[] { -4, -6, -12, -6, -14 });
                yield return new TestCaseData(new int[] { 49, 3, -7, 22, -17, 39, 43, -10, 46, -12, -37, -46, 39, -24, 33 }, new EvenNumber(), new int[] { 22, -10, 46, -12, -46, -24 });
                yield return new TestCaseData(new int[] { }, new EvenNumber(), new int[] { });

                #endregion
            }
        }

        [TestCaseSource(nameof(Data_For_Filter))]
        public void Filter_IPredicate(int[] source, IPredicate<int> predicate, int[] expectedArray) 
            => Assert.AreEqual(expectedArray, EnumerableExtension.Filter(source, predicate));

        [TestCaseSource(nameof(Data_For_Filter))]
        public void Filter_Predicate(int[] source, IPredicate<int> predicate, int[] expectedArray) 
            => Assert.AreEqual(expectedArray, EnumerableExtension.Filter(source, predicate.IsMatching));

        class EvenNumber : IPredicate<int>
        {
            public bool IsMatching(int item) => (item & 1) == 0;
        }

        #endregion

        #region Transform

        private static IEnumerable<TestCaseData> Data_For_Transform
        {
            get
            {
                #region TransformerIEEE754

                yield return new TestCaseData
                (
                    new double[] { -255.255, 255.255, 4294967295.0 },
                    new TransformerIEEE754(),
                    new string[] { "1100000001101111111010000010100011110101110000101000111101011100", "0100000001101111111010000010100011110101110000101000111101011100", "0100000111101111111111111111111111111111111000000000000000000000" }
                );

                yield return new TestCaseData
                (
                    new double[] { double.MinValue, double.MaxValue, double.Epsilon },
                    new TransformerIEEE754(),
                    new string[] { "1111111111101111111111111111111111111111111111111111111111111111", "0111111111101111111111111111111111111111111111111111111111111111", "0000000000000000000000000000000000000000000000000000000000000001" }
                );
                yield return new TestCaseData
                (
                    new double[] { double.NaN, double.NegativeInfinity, double.PositiveInfinity },
                    new TransformerIEEE754(),
                    new string[] { "1111111111111000000000000000000000000000000000000000000000000000", "1111111111110000000000000000000000000000000000000000000000000000", "0111111111110000000000000000000000000000000000000000000000000000" }
                );
                yield return new TestCaseData(new double[] { }, new TransformerIEEE754(), new string[] { });

                #endregion
            }
        }

        [TestCaseSource(nameof(Data_For_Transform))]
        public void Transform_ITransformer(double[] source, ITransformer<double, string> transformer, string[] expectedArray)
            => Assert.AreEqual(expectedArray, EnumerableExtension.Transform(source, transformer));

        [TestCaseSource(nameof(Data_For_Transform))]
        public void Transform_Converter(double[] source, ITransformer<double, string> transformer, string[] expectedArray)
            => Assert.AreEqual(expectedArray, EnumerableExtension.Transform(source, transformer.Transform));

        #endregion
    }
}
