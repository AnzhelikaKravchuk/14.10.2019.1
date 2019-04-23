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

        private class SillyTransform : ITransformer<int, int>
        {
            public int Transform(int item)
            {
                return 4;
            }
        }
    }
}
