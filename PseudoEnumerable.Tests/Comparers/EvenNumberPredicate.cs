using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoEnumerable.Tests.Comparers
{
    public class EvenNumberPredicate : IPredicate<int>
    {
        public bool IsMatching(int item)
        {
            if (item % 2 == 0)
            {
                return true;
            }

            return false;
        }
    }
}
