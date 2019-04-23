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
        [TestCase(arg1: new[] { 7, 1, 2, 3, 4, 5, 6, 7, 68, 69, 70, 15, 17 }, arg2: 7, arg3: new[] { 7, 7, 70, 17 })]
        [TestCase(arg1: new[] { 12, 45, 7, 30, 11, 15, 228, 547, 78, 3, 12, 8, 5 }, arg2: 5, arg3: new[] { 45, 15, 547, 5 })]
        [TestCase(arg1: new[] { 7, 1, 2, 3, 4, 5, 6, 7, 68, 69, 70, 15, 17 }, arg2: 1, arg3: new[] { 1, 15, 17 })]
        [TestCase(arg1: new[] { 7, 1, int.MaxValue, 3, 4, 5, 7, 69, int.MaxValue, 15 }, arg2: 5, arg3: new[] { 5, 15 })]
        [TestCase(arg1: new[] { 1, 1, 1, 1, 1 }, arg2: 1, arg3: new[] { 1, 1, 1, 1, 1 })]
        [TestCase(arg1: new[] { 1, 1, 1, 1, 1 }, arg2: 9, arg3: new int[] { })]
        public void FilterByKey_ValidArray_FilteredArray(IEnumerable<int> actual, int key, IEnumerable<int> expected)
        {
            var result = actual.Filter(new Contain(key));

            Assert.AreEqual(expected, result);
        }

        [TestCase(arg1: new[] { 7, 7, 70, -15, 17 },  arg2: new[] { 7, 7, 70, 17 })]
        public void FilterByKeyPredicate_ValidArray_FilteredArray(IEnumerable<int> actual,  IEnumerable<int> expected)
        {
            var result = actual.Filter(new Predicate<int>((i => i > 0)));

            Assert.AreEqual(expected, result);
        }

        [TestCase(arg1: new[] { 7, 7, 70, 17 }, arg2: new[] { "7", "7", "70", "17" })]
        public void TransformPredicate_ValidArray_StringArray(IEnumerable<int> actual, IEnumerable<string> expected)
        {
            var result = actual.Transform(new Converter<int,string>((i => i.ToString())));

            Assert.AreEqual(expected, result);
        }
    }
}
