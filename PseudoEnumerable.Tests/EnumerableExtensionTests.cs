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
        class Foo : ITransformer<int, string>
        {
            public string Transform(int item) => item.ToString();
        }

        [Test]
        public void Filter_DigitInNumber_WithConcreteNumber()
        {
            int[] g = new int[] {1, 23, 32};
            Foo foo = new Foo();
            string[] f = g.Transform(foo).ToArray();
        }
    }
}
