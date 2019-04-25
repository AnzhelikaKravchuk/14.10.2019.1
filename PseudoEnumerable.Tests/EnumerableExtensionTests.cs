using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PseudoEnumerable.Tests
{
    public class EnumerableExtensionTests
    {
        [TestCase(new int[] { 1, 4, 3, 2, 5, 6 }, ExpectedResult = new int[] { 2, 4, 6, 1, 3, 5 })]
        public IEnumerable<int> SortBy_FirstEven_returnsSortedSequence(int[] mas)
        {
            return mas.SortBy((x,y) => (x % 2 == 0 && y % 2 != 0) ? -1 : x - y);
        }
    }
}
