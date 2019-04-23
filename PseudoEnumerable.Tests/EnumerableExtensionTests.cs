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
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6 }, ExpectedResult = new int[] { 2, 4, 6 })]
        public IEnumerable<int> Filter_FiltersNumbers(IEnumerable<int> source)
            => EnumerableExtension.Filter(source, EvenNumbers);

        private bool EvenNumbers(int source)
            => source % 2 == 0;
    }
}
