using NumberExtensions;

using PseudoEnumerable.Interfaces;

namespace TrainingExtensions.Tests.ConvertorImplementations
{
    public class DoubleToBinaryString : ITransformer<double, string>
    {
        public string Transform(double source)
        {
            return source.TransformToBinary();
        }
    }
}