using PseudoEnumerable;
using NUnit.Framework;

namespace PseudoEnumerable.Tests
{
    [TestFixture()]
    public class EnumerableExtensionTests
    {
        [Test]
        public void OrderAccordingToTest()
        {
            var array = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };
            var expected = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            Assert.AreEqual(EnumerableExtension.OrderAccordingTo(array, (x, y) => x - y), expected);
        }

        [Test]
        public void FilterTest()
        {
            var array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var expected = new int[] { 2, 4, 6, 8, 10 };

            Assert.AreEqual(EnumerableExtension.Filter(array, x => x % 2 == 0), expected);
        }

        [Test()]
        public void TransformTest()
        {
            var array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var expected = new int[] { 2, 4, 6, 8, 10, 12, 14, 16, 18, 20 };

            Assert.AreEqual(EnumerableExtension.Transform(array, x => x * 2), expected);
        }
    }
}
