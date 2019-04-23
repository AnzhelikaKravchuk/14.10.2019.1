using System.Collections.Generic;
using NUnit.Framework;

namespace PseudoEnumerable.Tests
{
    [TestFixture]
    public class EnumerableExtensionTests
    {
        [TestCase(new[] { 1, -3, -6, 2, 10 }, ExpectedResult = new[] { 1, 2, 10 })]
        public IEnumerable<int> Filter_Predicate_CanFilter(int[] array) =>
            array.Filter(i => i > 0);

        [TestCase(new[] { 1, -3, -6, 2, 10 }, ExpectedResult = new[] { 1, 2, 10 })]
        public IEnumerable<int> Filter_IPredicate_CanFilter(int[] array) =>
            array.Filter(new CustomFilter());

        [TestCase(new[] { 1, -3, -6, 2, 10 }, ExpectedResult = new[] { "1", "-3", "-6", "2", "10" })]
        public IEnumerable<string> Transform_Transformer_CanTransform(int[] array) =>
            array.Transform(input => input.ToString() );

        [TestCase(new[] { 1, -3, -6, 2, 10 }, ExpectedResult = new[] { "1", "-3", "-6", "2", "10" })]
        public IEnumerable<string> Transform_ITransformer_CanTransform(int[] array) =>
            array.Transform(new CustomTransformer());
    }
}
