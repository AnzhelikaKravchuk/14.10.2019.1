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
        [Test]
        public void Filter_DigitInNumber_WithConcreteNumber()
        {
            int[] g = new int[] {1, 23, 32};
            int[] f = g.Filter(el => el < 5).ToArray();
        }
    }
}
