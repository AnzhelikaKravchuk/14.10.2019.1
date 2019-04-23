namespace PseudoEnumerable
{
    public interface ITransformer<TSource, TResult>
    {
        TResult Transform(TSource item);
    }
}