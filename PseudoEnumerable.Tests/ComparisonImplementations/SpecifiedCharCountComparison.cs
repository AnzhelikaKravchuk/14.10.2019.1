using System.Collections.Generic;

namespace TrainingExtensions.Tests.ComparisonImplementations
{
    public class SpecifiedCharCountComparison : IComparer<string>
    {
        private readonly char symbol;

        public SpecifiedCharCountComparison(char symbol)
        {
            this.symbol = symbol;
        }

        public int Compare(string x, string y)
        {
            if ((x == null) && (y == null))
            {
                return 0;
            }

            if (x == null)
            {
                return -1;
            }

            if (y == null)
            {
                return 1;
            }

            return this.GetSymbolCount(x).CompareTo(this.GetSymbolCount(y));
        }

        private int GetSymbolCount(string str)
        {
            var count = 0;

            foreach (var c in str)
            {
                if (c == this.symbol)
                {
                    count++;
                }
            }

            return count;
        }
    }
}