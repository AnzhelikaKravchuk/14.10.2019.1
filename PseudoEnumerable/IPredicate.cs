namespace PseudoEnumerable
{
    public interface IPredicate<in T>
    {
        bool IsMatching(T item);
    }
}