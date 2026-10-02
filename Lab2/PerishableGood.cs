// Represents a physical good with limited shelf life.
public class PerishableGood : PhysicalGood, IDiscountable 
{
    private int shelfLifeDays;
    private const decimal SurchargeFee = 0.40m;

    public int ShelfLifeDays
    {
        get { return shelfLifeDays; }
    }
    public bool IsOnSale
    {
        get { return ShelfLifeDays <= 3; }
    }
    public PerishableGood(
        string sku, 
        string name,
        decimal unitPrice,
        int quantityOnHand,
        double weightPounds,
        int shelfLifeDays)
        : base(sku, name, unitPrice, quantityOnHand, weightPounds)
    {
        if (shelfLifeDays < 0)
        {
            this.shelfLifeDays = 0;
        }
        else
        {
            this.shelfLifeDays = shelfLifeDays;
        }
    }

    public override string Category()
    {
        return "Perishable";
    }

    public override decimal HandlingFee()
    {
        return ShippingCost() + SurchargeFee;
    }

    public decimal SalePrice()
    {
        if (IsOnSale)
        {
            return UnitPrice * 0.70m;
        }

        return UnitPrice;
    }

    public override string Describe()
    {
        return base.Describe()
            + String.Format(", {0} days left", ShelfLifeDays);
    }

}