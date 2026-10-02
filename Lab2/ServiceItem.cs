// Represents a service measured by labor hours.
public class ServiceItem : StockItem, IDiscountable
{
    private double laborHours;

    public double LaborHours
    {
        get { return laborHours; }
    }

    public bool IsOnSale
    {
        get { return LaborHours >= 2.0; }
    }

    public ServiceItem(
        string sku,
        string name,
        decimal unitPrice,
        int quantityOnHand,
        double laborHours)
        : base(sku, name, unitPrice, quantityOnHand)
    {
        if (laborHours < 0)
        {
            this.laborHours = 0;
        }
        else
        {
            this.laborHours = laborHours;
        }
    }

    public override string Category()
    {
        return "Service";
    }

    public override decimal HandlingFee()
    {
        return 0m;
    }

    public decimal SalePrice()
    {
        if (IsOnSale)
        {
            return UnitPrice * 0.85m;
        }

        return UnitPrice;
    }

    public override string Describe()
    {
        return base.Describe()
            + String.Format(", {0:N1} labor hours", LaborHours);
    }
}
