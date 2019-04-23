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
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6 }, ExpectedResult = new int[] { 2, 4, 6 })]
        public IEnumerable<int> Filter_FiltersNumbers(IEnumerable<int> source)
            => EnumerableExtension.Filter(source, EvenNumbers);

        [TestCase(arg: new string[] { "some", "string" }, ExpectedResult = new int[] { 4, 6 })]
        public IEnumerable<int> Transform_TransformsNumbers(IEnumerable<string> source)
            => EnumerableExtension.Transform(source, new Lengths());

        private bool EvenNumbers(int source)
            => source % 2 == 0;
        
        internal class Lengths : ITransformer<string, int>
        {
            public int Transform(string item)
            {
                return item.Length;
            }
        }
    }
}
