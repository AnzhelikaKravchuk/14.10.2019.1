namespace PseudoEnumerable
{
    public interface IPredicate<TSource>
    {
        bool IsMatching(TSource number);
    }
}