namespace PseudoEnumerable
{
    public interface ITransformer<in TSource, out TResult>
    {
        TResult Transform(TSource item);
    }
}