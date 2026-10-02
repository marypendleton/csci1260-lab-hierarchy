// Represents a physical good with a warranty.
public class DurableGood : PhysicalGood
{
    private int warrantyMonths;

    public int WarrantyMonths
    {
        get { return warrantyMonths; }
    }

    public DurableGood(
        string sku,
        string name,
        decimal unitPrice,
        int quantityOnHand,
        double weightPounds,
        int warrantyMonths)
        : base(sku, name, unitPrice, quantityOnHand, weightPounds)
    {
        if (warrantyMonths < 0)
        {
            this.warrantyMonths = 0;
        }
        else
        {
            this.warrantyMonths = warrantyMonths;
        }
    }

    public override string Category()
    {
        return "Durable";
    }

    public override decimal HandlingFee()
    {
        return ShippingCost();
    }

    public override string Describe()
    {
        return base.Describe()
            + String.Format(", {0} month warranty", WarrantyMonths);
    }
}