// Represents a stock item that has weight shipping cost.
public abstract class  PhysicalGood : StockItem
{
    private double weightPounds;
    private const decimal HandlingRate = 0.60m;
    
    public double WeightPounds
    {
        get { return weightPounds; }
    }

    protected PhysicalGood(
        string sku, string name, decimal unitPrice, int quantityOnHand, double weightPounds)
        : base(sku, name, unitPrice, quantityOnHand)
    {
        if (weightPounds < 0)
        {
            this.weightPounds = 0;
        }
        else
        {
            this.weightPounds = weightPounds;
        }
    }

    public decimal ShippingCost()
    {
        return (decimal)weightPounds * HandlingRate;
    }

    public override string Describe()
    {
        return base.Describe()
            + String.Format(", {0:N1} lb", WeightPounds);

    }
}
