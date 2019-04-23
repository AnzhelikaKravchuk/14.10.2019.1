using System.Collections.Generic;
using NUnit.Framework;

namespace PseudoEnumerable.Tests
{
    [TestFixture]
    public class EnumerableExtensionTests
    {
        [TestCase(new[] { 1, -3, -6, 2, 10 }, ExpectedResult = new[] { 1, 2, 10 })]
        public IEnumerable<int> Transform_Predicate_CanFilter(int[] array) =>
            array.Filter(i => i > 0);

        [TestCase(new[] { 1, -3, -6, 2, 10 }, ExpectedResult = new[] { 1, 2, 10 })]
        public IEnumerable<int> Transform_IPredicate_CanFilter(int[] array) =>
            array.Filter(new CustomFilter());
    }



}
