using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PseudoEnumerable.Tests
{
    public class FilterContainsNumber : IPredicate<int>
    {
        private int key;

        public FilterContainsNumber(int keyNumber) => key = keyNumber;

        public bool IsMatching(int number) => number.ToString().Contains(key.ToString());
    }

    public class FilterIsEven : IPredicate<int>
    {
        public bool IsMatching(int number) => number % 2 == 0;
    }

    public class EnumerableExtensionTests
    {
        [TestCase(new[] { 1, 2, 3, 14, 21, 1, -12, -5 }, ExpectedResult = new[] { 2, 14, -12 })]
        [TestCase(new[] { 2341, 227, -32, 33, 144, 21, 1, 212, -5 }, ExpectedResult = new[] { -32, 144, 212 })]
        public IEnumerable<int> Filter_FilterIsEvenWithConcreteArray(int[] array) => array.Filter(new FilterIsEven());

        [TestCase(new[] { 1, 2, 3, 14, 21, 1, -12, -5 }, ExpectedResult = new[] { 2, 14, -12 })]
        [TestCase(new[] { 2341, 227, -32, 33, 144, 21, 1, 212, -5 }, ExpectedResult = new[] { -32, 144, 212 })]
        public IEnumerable<int> Filter_FilterDelegateIsEvenWithConcreteArray(int[] array)
        {
            Predicate<int> predicate = new FilterIsEven().IsMatching;
            return array.Filter(predicate);
        }

        [TestCase(new double[] { 523, 0.234, double.PositiveInfinity },
           ExpectedResult = new[]
           {
            "пять два три",
            "нуль точка два три четыре",
            "положительная бесконечность"
           })]
        public IEnumerable<string> Transform_TransformToRussinWordsWith(double[] array) => array.Transform(new RusssianTransformer());

        [TestCase(new double[] { 523, 0.234, double.PositiveInfinity },
           ExpectedResult = new[]
           {
            "пять два три",
            "нуль точка два три четыре",
            "положительная бесконечность"
           })]
        public IEnumerable<string> Transform_TransformWithDelegateToRussinWordsWith(double[] array) => array.Transform(new Converter<double,string>(new RusssianTransformer().Transform));
    }

    public interface IDictionaryCreater
    {
        Dictionary<char, string> GetWords();

        Dictionary<double, string> GetSpecialDoubles();
    }

    public abstract class Transformer : ITransformer<double, string>
    {
        private IDictionaryCreater dictionaryCreater;

        protected IDictionaryCreater DictionaryCreater { get => dictionaryCreater; set => dictionaryCreater = value; }

        public virtual string Transform(double number)
        {
            Dictionary<double, string> specialNumbers = DictionaryCreater?.GetSpecialDoubles();
            if (specialNumbers.TryGetValue(number, out string result))
            {
                return result;
            }

            Dictionary<char, string> words = DictionaryCreater?.GetWords();
            return GetWordFormat(number, words);
        }

        private string GetWordFormat(double number, Dictionary<char, string> words)
        {
            var numbersString = number.ToString(CultureInfo.InvariantCulture);
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < numbersString.Length; i++)
            {
                if (i != 0)
                {
                    stringBuilder.Append(" ");
                }

                stringBuilder.Append(words[numbersString[i]]);
            }

            return stringBuilder.ToString();
        }
    }

    public class RusssianTransformer : Transformer, IDictionaryCreater
    {
        public RusssianTransformer() => DictionaryCreater = this;

        public Dictionary<double, string> GetSpecialDoubles() => new Dictionary<double, string>
        {
            [double.PositiveInfinity] = "положительная бесконечность",
            [double.NegativeInfinity] = "отрицательная бесконечность",
            [double.NaN] = "не является числом"
        };

        public Dictionary<char, string> GetWords() => new Dictionary<char, string>
        {
            ['0'] = "нуль",
            ['1'] = "один",
            ['2'] = "два",
            ['3'] = "три",
            ['4'] = "четыре",
            ['5'] = "пять",
            ['6'] = "шесть",
            ['7'] = "семь",
            ['8'] = "восемь",
            ['9'] = "девять",
            ['-'] = "минус",
            [','] = "точка",
            ['.'] = "точка",
            ['e'] = "экспонента",
            ['+'] = "плюс"
        };
    }

}
