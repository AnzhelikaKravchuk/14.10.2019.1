using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace PseudoEnumerable.Tests
{
    [TestFixture]
    public class EnumerableExtensionTests
    {
        #region Filter

        [TestCase(arg: new int[] { }, ExpectedResult = new int[] { })]
        [TestCase(arg: new[] { int.MinValue }, ExpectedResult = new int[] { })]
        [TestCase(arg: new[] { -1, -2, -3, -4, -5 }, ExpectedResult = new int[] { })]
        [TestCase(arg: new[] { int.MaxValue, -7, -10, 12 }, ExpectedResult = new int[] { int.MaxValue, 12})]
        [TestCase(new[] { 1, -3, -6, 2, 10 }, ExpectedResult = new[] { 1, 2, 10 })]
        public IEnumerable<int> Filter_Predicate_CanFilter(int[] array) =>
            array.Filter(i => i > 0);

        [TestCase(arg: new string[] { }, ExpectedResult = new string[] { })]
        [TestCase(arg: new[] { "11", "22", "33" }, ExpectedResult = new string[] { })]
        [TestCase(arg: new[] { "111", "222", "333"}, ExpectedResult = new [] { "111", "222", "333" })]
        [TestCase(arg: new[] { "111222333" }, ExpectedResult = new[] { "111222333" })]
        [TestCase(arg: new[] { "", "q", "qqq", "qqqqq", "qqqqqqq" }, ExpectedResult = new[] { "qqq", "qqqqq", "qqqqqqq" })]
        public IEnumerable<string> Filter_Predicate_Strings_CanFilter(string[] array) =>
            array.Filter(s => s.Length > 2);

        [TestCase(arg: new int[] { }, ExpectedResult = new int[] { })]
        [TestCase(arg: new[] { int.MinValue }, ExpectedResult = new int[] { })]
        [TestCase(arg: new[] { -1, -2, -3, -4, -5 }, ExpectedResult = new int[] { })]
        [TestCase(arg: new[] { int.MaxValue, -7, -10, 12 }, ExpectedResult = new int[] { int.MaxValue, 12 })]
        [TestCase(new[] { 1, -3, -6, 2, 10 }, ExpectedResult = new[] { 1, 2, 10 })]
        public IEnumerable<int> Filter_IPredicate_CanFilter(int[] array) =>
            array.Filter(new CustomFilter());

        [Test]
        public void Filter_ArrayIsNull_Throw_ArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(
                () => EnumerableExtension.Filter<string>(null, s => s.Length > 5));

        [Test]
        public void Filter_PredicateIsNull_Throw_ArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(
                () => EnumerableExtension.Filter(new string[] { "123" }, (IPredicate<string>)null));

        #endregion

        #region Transform

        [TestCase(new int[] { }, ExpectedResult = new string[] { })]
        [TestCase(new[] { 1 }, ExpectedResult = new[] { "1" })]
        [TestCase(new[] { int.MaxValue }, ExpectedResult = new[] { "2147483647" })]
        [TestCase(new[] { 1, 2, 3 }, ExpectedResult = new[] { "1", "2", "3" })]
        [TestCase(new[] { 1, -3, -6, 2, 10 }, ExpectedResult = new[] { "1", "-3", "-6", "2", "10" })]
        public IEnumerable<string> Transform_Transformer_CanTransform(int[] array) =>
            array.Transform(input => input.ToString());
        
        [TestCase(arg: new string[] { }, ExpectedResult = new int[] { })]
        [TestCase(arg: new[] { "qqqqqqqqqqqqqqq", "" }, ExpectedResult = new int[] { 15, 0 })]
        [TestCase(arg: new[] { "", "", "", "", "" }, ExpectedResult = new int[] { 0, 0, 0, 0, 0 })]
        [TestCase(arg: new[] { "", "q", "qqq", "qqqqq", "qqqqqqq" }, ExpectedResult = new[] { 0, 1, 3, 5, 7 })]
        public IEnumerable<int> Transform_ITransformer_StringToLength_CanTransform(string[] array) =>
            array.Transform(s => s.Length);

        [TestCase(new int[] { }, ExpectedResult = new string[] { })]
        [TestCase(new[] { 1 }, ExpectedResult = new[] { "1" })]
        [TestCase(new[] { int.MaxValue }, ExpectedResult = new[] { "2147483647" })]
        [TestCase(new[] { 1, 2, 3 }, ExpectedResult = new[] { "1", "2", "3" })]
        [TestCase(new[] { 1, -3, -6, 2, 10 }, ExpectedResult = new[] { "1", "-3", "-6", "2", "10" })]
        public IEnumerable<string> Transform_ITransformer_CanTransform(int[] array) =>
            array.Transform(new CustomTransformer());

        [Test]
        public void Transform_ArrayIsNull_Throw_ArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(
                () => EnumerableExtension.Transform<string, int>(null, s => s.Length));

        [Test]
        public void Transform_TransformerIsNull_Throw_ArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(
                () => EnumerableExtension.Transform<string, int>(
                    new string[]{"123"}, (ITransformer<string, int>)null));

        #endregion

        #region SortBy

        [TestCase(arg: new[] { "55555", "4444", "333", "aaa", "22", "1" },
            ExpectedResult = new[] { "1", "22", "333", "aaa", "4444", "55555" })]
        [TestCase(arg: new[] { "111", "2", "0000" }, ExpectedResult = new string[] { "2", "111", "0000" })]
        [TestCase(arg: new[] { "1", "2", "3" }, ExpectedResult = new string[] { "1", "2", "3" })]
        [TestCase(arg: new[] { "", "", "" }, ExpectedResult = new string[] { "", "", "" })]
        public IEnumerable<string> SortBy_Strings_DelegateComparer_CanSort(string[] array) =>
            EnumerableExtension.SortBy(array, (a, b) => a.Length.CompareTo(b.Length));

        [TestCase(arg: new int[] { }, ExpectedResult = new int[] { })]
        [TestCase(arg: new[] { 0 }, ExpectedResult = new[] { 0 })]
        [TestCase(arg: new[] { 7, 2,  -1,  6 }, ExpectedResult = new[] { -1, 2, 6, 7 })]
        [TestCase(arg: new[] { 60, 10, 20, 70 }, ExpectedResult = new[] { 10, 20, 60, 70 })]
        [TestCase(arg: new[] { 1000, 100, 10, 1 }, ExpectedResult = new[] { 1, 10, 100, 1000 })]
        [TestCase(arg: new[] { 60, -89090, 70, 8000, 800, 7 }, ExpectedResult = new[] { -89090, 7, 60, 70,  800, 8000 })]
        public IEnumerable<int> SortBy_Integers_DelegateComparer_CanSort(int[] array) =>
            EnumerableExtension.SortBy(array, (a, b ) => a.CompareTo(b));

        [TestCase(arg: new[] { "22", "4444", "333", "55555", "aaa",  "1" },
            ExpectedResult = new[] { "55555", "4444", "333", "aaa", "22", "1"})]
        [TestCase(arg: new[] { "111", "2", "0000" }, ExpectedResult = new[] { "0000", "111", "2" })]
        [TestCase(arg: new[] { "1", "2", "3" }, ExpectedResult = new[] { "1", "2", "3" })]
        [TestCase(arg: new[] { "", "", "" }, ExpectedResult = new[] { "", "", "" })]
        [TestCase(arg: new string[] { }, ExpectedResult = new string[] { })]
        public IEnumerable<string> SortBy_Strings_CustomByDescendingLengthComparer_CanSort(string[] array) =>
            EnumerableExtension.SortBy(array, new CustomComparer());

        [Test]
        public void SortBy_ArrayIsNull_Throw_ArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(
                () => EnumerableExtension.SortBy<string>(
                    null, (a, b)=> a.Length.CompareTo(b.Length)));

        #endregion

    }
}
