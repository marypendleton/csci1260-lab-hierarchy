// Represents one count of a shelf location and its value.
public class ShelfCount :
    IEquatable<ShelfCount>,
    IComparable<ShelfCount>
{
    private string aisle;
    private int slot;
    private double valueOnHand;

    public string Aisle
    {
        get { return aisle; }
    }

    public int Slot
    {
        get { return slot; }
    }

    public double ValueOnHand
    {
        get { return valueOnHand; }
    }

    public ShelfCount(
        string aisle,
        int slot,
        double valueOnHand)
    {
        this.aisle = aisle;
        this.slot = slot;
        this.valueOnHand = valueOnHand;
    }

    public bool Equals(ShelfCount other)
    {
        if (other == null)
        {
            return false;
        }

        return string.Equals(
            Aisle,
            other.Aisle,
            StringComparison.Ordinal)
            && Slot == other.Slot;
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as ShelfCount);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Aisle, Slot);
    }

    public int CompareTo(ShelfCount other)
    {
        if (other == null)
        {
            return 1;
        }

        int byAisle = string.Compare(
            Aisle,
            other.Aisle,
            StringComparison.Ordinal);

        if (byAisle != 0)
        {
            return byAisle;
        }

        return Slot.CompareTo(other.Slot);
    }

    public override string ToString()
    {
        return String.Format(
            "{0,-6} #{1} {2,8:N2}",
            Aisle,
            Slot,
            ValueOnHand);
    }
}
