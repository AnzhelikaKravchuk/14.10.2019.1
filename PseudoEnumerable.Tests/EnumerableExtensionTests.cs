using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoEnumerable.Tests
{
    using NUnit.Framework;

    using PseudoEnumerable.Tests.Comparers;

    [TestFixture]
    public class EnumerableExtensionTests
    {
        [Test]
        public void Filter_EvenNumberFilterTest()
        {
            IEnumerable<int> actualArray = new int[] { 2, -10, 13, 55, -33, 22 };
            IEnumerable<int> expectedArray = new int[] { 2, -10, 22 };

            actualArray = actualArray.Filter(new EvenNumberPredicate());

            CollectionAssert.AreEqual(expectedArray, actualArray);
        }

        [Test]
        public void Filter_EvenNumberFilterPredicateDelegateTest()
        {
            IEnumerable<int> actualArray = new int[] { 2, -10, 13, 55, -33, 22 };
            IEnumerable<int> expectedArray = new int[] { 2, -10, 22 };
            actualArray = actualArray.Filter(this.IsEven);

            CollectionAssert.AreEqual(expectedArray, actualArray);
        }

        public bool IsEven(int item)
        {
            if (item % 2 == 0)
            {
                return true;
            }

            return false;
        }
    }
}
