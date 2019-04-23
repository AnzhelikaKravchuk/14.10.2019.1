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
        [TestCase(new int[] { 1, 2, 3, 4, -56, 85, 9 }, 5, ExpectedResult = new int[] { -56, 85 })]
        [TestCase(new int[] { 1, 1, 1, 1, 1, 1, 1 }, 1, ExpectedResult = new int[] { 1, 1, 1, 1, 1, 1, 1 })]
        [TestCase(new int[] { 1, 22, 33, 44, 5, 66 }, 7, ExpectedResult = new int[] { })]
        [TestCase(new int[] { int.MaxValue, 0, int.MaxValue }, 4, ExpectedResult = new int[] { int.MaxValue, int.MaxValue })]
        public IEnumerable<int> FilterIsContainNumberCustomTests(IEnumerable<int> array, int digit)
            => array.Filter(new ContainNumberPredicate(digit));

        [TestCase(new int[] { 1, 2, 3, 4, -56, -85, -9 }, ExpectedResult = new int[] { 1, 2, 3, 4})]
        [TestCase(new int[] { 1, 1, 1, 1, -2, 1, 1 }, ExpectedResult = new int[] { 1, 1, 1, 1, 1, 1 })]
        [TestCase(new int[] { 1, 22, 33, 44, 5, 66 },ExpectedResult = new int[] { 1, 22, 33, 44, 5, 66 })]
        [TestCase(new int[] { int.MaxValue, 0, int.MaxValue }, ExpectedResult = new int[] { int.MaxValue, int.MaxValue })]
        public IEnumerable<int> FilterIsContainNumberTests(IEnumerable<int> array)
        => array.Filter(new Predicate<int>((i => i>0)));
    }
}
