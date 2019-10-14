using ConversionExtensions.DoubleIntoVerbalFormat;

using PseudoEnumerable.Interfaces;

namespace TrainingExtensions.Tests.ConvertorImplementations
{
    public enum ConversionDictionary
    {
        EnglishConversion,

        RussianConversion
    }

    public class DoubleIntoVerbalFormat : ITransformer<double, string>
    {
        private readonly Transformer transformer;

        public DoubleIntoVerbalFormat(ConversionDictionary dictionary)
        {
            switch (dictionary)
            {
                case ConversionDictionary.EnglishConversion:
                    this.transformer = new Transformer(new EnglishConversionDictionary());
                    break;
                case ConversionDictionary.RussianConversion:
                    this.transformer = new Transformer(new RussianConversionDictionary());
                    break;
            }
        }

        public string Transform(double source)
        {
            return this.transformer.TransformToWords(source);
        }
    }
}