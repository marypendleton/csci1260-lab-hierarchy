// Sorts shelf counts from the highest value to the lowest value.
public class HighestValueFirst : IComparer<ShelfCount>
{
    public int Compare(ShelfCount a, ShelfCount b)
    {
        if (a.ValueOnHand != b.ValueOnHand)
        {
            return b.ValueOnHand.CompareTo(a.ValueOnHand);
        }

        return a.CompareTo(b);
    }
}