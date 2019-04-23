using System;
using System.Collections.Generic;
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
    }

}
