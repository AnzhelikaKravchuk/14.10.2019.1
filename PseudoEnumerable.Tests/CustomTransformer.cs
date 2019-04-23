namespace PseudoEnumerable.Tests
{
    class CustomTransformer : ITransformer<int, string>
    {
        public string Transform(int item)
        {
            return item.ToString();
        }
    }
}
