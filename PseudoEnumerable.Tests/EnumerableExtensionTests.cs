using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using static PseudoEnumerable.EnumerableExtension;

namespace PseudoEnumerable.Tests
{
    [TestFixture]
    public class EnumerableExtensionTests
    {
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6 }, new int[] { 2, 4, 6 })]
        [TestCase(new int[] { 0, 0, 0 }, new int[] { 0, 0, 0 })]
        [TestCase(new int[] { 1, 3, 5, 7, }, new int[] { })]
        [TestCase(new int[] { int.MaxValue, int.MinValue }, new int[] { int.MinValue })]
        public void Filter_InputValidArgumentsInt_ValidResult(int[] array, int[] expectedArray)
        {
            List<int> result = new List<int>(array.Filter(i => i % 2 == 0));

            Assert.AreEqual(expectedArray, result);
        }

        [TestCase(new string[] { "string", "a a a", "int", "double" }, new string[] { "string", "double" })]
        [TestCase(new string[] { "asd", "awdws", "int", "wdawdwd" }, new string[] {  })]
        [TestCase(new string[] {}, new string[] {})]
        public void Filter_InputValidArgumentsWithString_ValidResult(string[] array, string[] expectedArray)
        {
            List<string> result = new List<string>(array.Filter(i => i.Length % 2 == 0));

            Assert.AreEqual(expectedArray, result);
        }

        [TestCase(new double[] { 12, 23, 34, 45, 56, 67 }, new string[] { "12", "23", "34", "45", "56", "67" })]
        [TestCase(new double[] { 12.5, 54.5, 111.1 }, new string[] { "12,5", "54,5", "111,1"})]
        public void Transform_InputValidArgumentsWithIntToString_ValidResult(double[] array, string[] expectedArray)
        {
            List<string> result = new List<string>(array.Transform(i => i.ToString()));

            Assert.AreEqual(expectedArray, result);
        }

        [TestCase(new string[] { "12", "23", "34", "45", "56", "67" }, new int[] { 12, 23, 34, 45, 56, 67 })]
        public void Transform_InputValidArgumentsWithStringToInt_ValidResult(string[] array, int[] expectedArray)
        {
            List<int> result = new List<int>(array.Transform(i => int.Parse(i)));

            Assert.AreEqual(expectedArray, result);
        }

        [TestCase(new string[] { "aaa", "aaaa", "a", "aaaaa", "aa"  }, new string[] { "a", "aa", "aaa", "aaaa", "aaaaa" })]
        [TestCase(new string[] { "aaa", "aaa", "aaa", "aaa", "aaa" }, new string[] { "aaa", "aaa", "aaa", "aaa", "aaa" })]
        public void SortBy_SortOfStringsByTheirLength_ValidResult(string[] array, string[] expectedArray)
        {
            List<string> result = new List<string>(array.SortBy((i, j) => 
            {
                if (i.Length > j.Length)
                {
                    return 1;
                }
                else if (i.Length < j.Length)
                {
                    return -1;
                }

                return 0;
            } ));

            Assert.AreEqual(expectedArray, result);
        }

    }
}
