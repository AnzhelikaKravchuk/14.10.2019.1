using System.Collections.Generic;

namespace PseudoEnumerable.Tests
{
    public class CustomComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            return y.Length.CompareTo(x.Length);
        }
    }
}
