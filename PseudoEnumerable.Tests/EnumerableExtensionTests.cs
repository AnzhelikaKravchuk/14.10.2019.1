using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using PseudoEnumerable.Interfaces;

using TrainingExtensions.Tests.ComparisonImplementations;
using TrainingExtensions.Tests.ConvertorImplementations;
using TrainingExtensions.Tests.PredicatesImplementations;

//// ReSharper disable InconsistentNaming
namespace PseudoEnumerable.Tests
{
    public class EnumerableExtensionTests
    {
        [TestCase(new[] { 1, 2, 3, 4, 5 }, ExpectedResult = new[] { 5 })]
        [TestCase(new[] { 1642, 2805, 3483, 4325, 1555, -4804, 1784, 1492 }, ExpectedResult = new[] { 2805, 4325, 1555 })]
        [TestCase(new[] { 1570, 3170, 2249, 3012, 2757, 2629, 1336, -3417 }, ExpectedResult = new[] { 1570, 2757 })]
        [TestCase(new[] { 3578, 1300, 2097, 3705, 4720, 3809, 2887, 1485 }, ExpectedResult = new[] { 3578, 3705, 1485 })]
        [TestCase(new[] { 1931, 4798, 2052, -2228, 1600, 2719, 3015, -1525 }, ExpectedResult = new[] { 2052, 3015, -1525 })]
        public static IEnumerable<int> FilterTest_ContainsDigit_DigitEqualsFive(int[] source)
        {
            IPredicate<int> predicate = new ContainsDigit(5);
            return source.Filter(predicate);
        }

        [TestCase(new[] { 1, 2, 3, 4, 5 }, ExpectedResult = new[] { 2, 4 })]
        [TestCase(new[] { 1642, 2805, 3483, 4325, 1555, -4804, 1784, 1492 }, ExpectedResult = new[] { 1642, -4804, 1784, 1492 })]
        [TestCase(new[] { 1570, 3170, 2249, 3012, 2757, 2629, 1336, -3417 }, ExpectedResult = new[] { 1570, 3170, 3012, 1336 })]
        [TestCase(new[] { 3578, 1300, 2097, 3705, 4720, 3809, 2887, 1485 }, ExpectedResult = new[] { 3578, 1300, 4720 })]
        [TestCase(new[] { 1931, 4798, 2052, -2228, 1600, 2719, 3015, -1525 }, ExpectedResult = new[] { 4798, 2052, -2228, 1600 })]
        public static IEnumerable<int> FilterTests_EvenNumberPredicate(int[] source)
        {
            IPredicate<int> predicate = new MultiplicityOfAGivenDigit(2);
            return source.Filter(predicate);
        }

        [TestCase(new object[] { "aa", "avv", "av" }, ExpectedResult = new[] { "aa", "av", "avv" })]
        [TestCase(new object[] { "abc", "bbvv", "ddddaddv" }, ExpectedResult = new[] { "abc", "ddddaddv", "bbvv" })]
        [TestCase("abvvvv", "bbvvv", "aa", "ddddaddv", "gggvv", ExpectedResult = new[] { "aa", "ddddaddv", "gggvv", "bbvvv", "abvvvv" })]
        public static IEnumerable<string> OrderAccordingToTest_SpecifiedChar(params string[] source)
        {
            IComparer<string> comparer = new SpecifiedCharCountComparison('v');
            return source.OrderAccordingTo(comparer);
        }

        [TestCase(new object[] { "aa", "aaa", "a" }, ExpectedResult = new[] { "a", "aa", "aaa" })]
        [TestCase("1111", "11", "11111", "1", ExpectedResult = new[] { "1", "11", "1111", "11111" })]
        [TestCase("9999", "999", "9999999", "111", "22222222", ExpectedResult = new[] { "999", "111", "9999", "9999999", "22222222" })]
        public static IEnumerable<string> OrderAccordingToTest_StringLength(params string[] source)
        {
            IComparer<string> comparer = new StringLengthComparison();
            return source.OrderAccordingTo(comparer);
        }

        [TestCase(new[] { -255.255, 255.255 }, ExpectedResult = new[] { "1100000001101111111010000010100011110101110000101000111101011100", "0100000001101111111010000010100011110101110000101000111101011100" })]
        [TestCase(new[] { -0.0, 0.0 }, ExpectedResult = new[] { "1000000000000000000000000000000000000000000000000000000000000000", "0000000000000000000000000000000000000000000000000000000000000000" })]
        [TestCase(new[] { 4294967295.0, double.MinValue, double.MaxValue, double.Epsilon }, ExpectedResult = new[] { "0100000111101111111111111111111111111111111000000000000000000000", "1111111111101111111111111111111111111111111111111111111111111111", "0111111111101111111111111111111111111111111111111111111111111111", "0000000000000000000000000000000000000000000000000000000000000001" })]
        [TestCase(new[] { double.NaN, double.NegativeInfinity, double.PositiveInfinity }, ExpectedResult = new[] { "1111111111111000000000000000000000000000000000000000000000000000", "1111111111110000000000000000000000000000000000000000000000000000", "0111111111110000000000000000000000000000000000000000000000000000" })]
        public static IEnumerable<string> TransformTest_TransFormToBinaryStrings(double[] source)
        {
            ITransformer<double, string> convertor = new DoubleToBinaryString();
            return source.Transform(convertor);
        }

        [TestCase(new[] { 0.1d, -23.809d }, ExpectedResult = new[] { "zero point one", "minus two three point eight zero nine" })]
        [TestCase(new[] { 1.0d, 2.0d, double.NegativeInfinity, double.PositiveInfinity }, ExpectedResult = new[] { "one", "two", "Negative infinity", "Positive infinity" })]
        [TestCase(new[] { 0.0d, double.NaN, -0.123456789d }, ExpectedResult = new[] { "zero", "Not a number", "minus zero point one two three four five six seven eight nine" })]
        public static IEnumerable<string> TransformTest_TransFormToEnglishWords(double[] source)
        {
            ITransformer<double, string> convertor = new DoubleIntoVerbalFormat(ConversionDictionary.EnglishConversion);
            return source.Transform(convertor);
        }

        [TestCase(new[] { 0.1d, -23.809d }, ExpectedResult = new[] { "ноль запятая один", "минус два три запятая восемь ноль девять" })]
        [TestCase(new[] { 1.0d, 2.0d, double.NegativeInfinity, double.PositiveInfinity }, ExpectedResult = new[] { "один", "два", "Отрицательная бесконечность", "Положительная бесконечность" })]
        [TestCase(new[] { 0.0d, double.NaN, -0.123456789d }, ExpectedResult = new[] { "ноль", "Не число", "минус ноль запятая один два три четыре пять шесть семь восемь девять" })]
        public static IEnumerable<string> TransformTest_TransFormToRussianWords(double[] source)
        {
            ITransformer<double, string> convertor = new DoubleIntoVerbalFormat(ConversionDictionary.RussianConversion);
            return source.Transform(convertor);
        }

        [Test]
        public void FindMaxTest_EmptyArray()
        {
            Assert.Throws<ArgumentException>(() => new int[] { }.Max());
        }

        [TestCase(null)]
        public void FindMaxTest_ThrowsArgumentNullException(int[] arr)
        {
            Assert.Throws<ArgumentNullException>(() => arr.Max());
        }

        [TestCase(new[] { 9, 4, 7, 6, 5, 3, 8 }, ExpectedResult = 9)]
        [TestCase(new[] { 4, 7, -1, 5, 3, 8 }, ExpectedResult = 8)]
        [TestCase(new[] { -1, 0, 1, 2, 3 }, ExpectedResult = 3)]
        [TestCase(new[] { 4, 4, 3, 2, -1 }, ExpectedResult = 4)]
        [TestCase(new[] { 1 }, ExpectedResult = 1)]
        public int MaxTest(int[] arr)
        {
            return arr.Max();
        }
    }
}