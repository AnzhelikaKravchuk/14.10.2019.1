using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoEnumerable
{
    public class MultiplicationByTwoTransformer : ITransformer<int, int>
    {
        public int Transform(int item)
        {
            return item * 2;
        }
    }
}
