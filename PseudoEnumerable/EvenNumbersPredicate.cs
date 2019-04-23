using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoEnumerable
{
    public class EvenNumbersPredicate : IPredicate<int>
    {
        public bool IsMatching(int item)
        {
            return item % 2 == 0;
        }
    }
}
