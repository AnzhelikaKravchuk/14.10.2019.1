using DoubleNumberTransformer;

namespace PseudoEnumerable
{
    public class TransformerIEEE754 : ITransformer<double, string>
    {
        public string Transform(double value)
        {
            return value.TransformToIEEE754();
        }
    }
}
