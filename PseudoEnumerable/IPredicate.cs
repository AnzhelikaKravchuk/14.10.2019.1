namespace PseudoEnumerable
{
    public interface IPredicate<in TSource>
    {
        bool IsMatching(TSource number);
    }
}