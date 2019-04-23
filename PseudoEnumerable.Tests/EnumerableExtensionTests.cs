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
        [TestCase(new int[] { 3, 4, 12, 15, 17 },
            ExpectedResult = new int[] { 4, 12 })]
        public IEnumerable<int> Filter_FilterByContentDigitTests(int[] array)
        {
            var result = new List<int>();
            foreach (var item in array.Filter(new EvenNumbersPredicate()))
            {
                result.Add(item);
            }

            return result.ToArray();
        }

        [TestCase(new int[] { 3, 4, 12, 15, 17 },
            ExpectedResult = new int[] { 4, 12 })]
        public IEnumerable<int> Filter_Delegate(int[] array)
        {
            Predicate<int> predicate = delegate (int x) { return x % 2 == 0; };
            var result = new List<int>();
            foreach (var item in array.Filter(predicate))
            {
                result.Add(item);
            }

            return result.ToArray();
        }

        [TestCase(new int[] { 3, 4, 12, 15 },
            ExpectedResult = new int[] { 6, 8, 24, 30 })]
        public IEnumerable<int> Transformer_Tests(int[] array)
        {     
            var result = new List<int>();
            foreach (var item in array.Transform(new MultiplicationByTwoTransformer()))
            {
                result.Add(item);
            }

            return result.ToArray();
        }

    }
}
