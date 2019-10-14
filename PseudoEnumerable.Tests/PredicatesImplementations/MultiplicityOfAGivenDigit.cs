using PseudoEnumerable.Interfaces;


namespace TrainingExtensions.Tests.PredicatesImplementations
{
    public class MultiplicityOfAGivenDigit : IPredicate<int>
    {
        private readonly int digit;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MultiplicityOfAGivenDigit" /> class.
        /// </summary>
        /// <param name="digit"></param>
        public MultiplicityOfAGivenDigit(int digit)
        {
            this.digit = digit;
        }

        public bool IsMatching(int value)
        {
            return value % this.digit == 0;
        }
    }
}