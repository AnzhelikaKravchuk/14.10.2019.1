namespace PseudoEnumerable.Tests
{
    class CustomFilter : IPredicate<int>
    {

        public bool IsMatching(int item)
        {
            return item > 0;
        }
    }
}
