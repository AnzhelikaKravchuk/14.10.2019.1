using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace PseudoEnumerable.Tests
{
    [TestFixture]
    public class EnumerableExtensionTests
    {
        IPredicate<int> predicate;
 
        [TestCase(new int[] { 2, 77, 43, -12, 0, 20, -31 }, ExpectedResult = new int[] { 2, -12, 0, 20 })]
        [TestCase(new int[] { 1, 333, 4, 55, -1, 89, 1 }, ExpectedResult = new int[] { 4 })]
        [TestCase(new int[] { int.MinValue, int.MaxValue }, ExpectedResult = new int[] { int.MinValue })]
        public int[] EvenNumberTest(int[] array)
        {
            predicate = new EvenNumber();
            Predicate<int> pred = predicate.IsMatching;
            return EnumerableExtension.Filter(array, pred).ToArray();
        }
   
    }

    public class EvenNumber : IPredicate<int>
    {
        public bool IsMatching(int number) => Math.Abs(number % 2) == 0;
    }
    
}
