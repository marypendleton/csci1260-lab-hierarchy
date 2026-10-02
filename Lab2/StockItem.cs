// Represents the shared data and behavior of all stock items in the inventory system.
public abstract class StockItem: IReportable
{
    private string sku;
    private string name;
    private decimal unitPrice;
    private int quantityOnHand;
    private List<StockMovement> history;
    private int nextSeq;

    public string Sku
    {
        get { return sku; }
    }

    public string Name
    {
        get { return name; }
    }

    public decimal UnitPrice
    {
        get { return unitPrice; }
    }

    public int QuantityOnHand
    {
        get { return quantityOnHand; }
    }
    public int MoveCount
    {
        get { return history.Count; }
    }

    protected StockItem(
        string sku,
        string name,
        decimal unitPrice,
        int quantityOnHand)
    {
        this.sku = sku;
        this.name = name;
       
        if (unitPrice < 0)
        {
            this.unitPrice = 0;
        }
        else
        {
            this.unitPrice = unitPrice;
        }
        if (quantityOnHand < 0)
        {
            this.quantityOnHand = 0;
        }
        else
        {
            this.quantityOnHand = quantityOnHand;
        }
        
        history = new List<StockMovement>();
        nextSeq = 1;
    }

    public abstract string Category();
    public abstract decimal HandlingFee();
    public decimal ExtendedValue()
    {
        return (UnitPrice + HandlingFee()) * QuantityOnHand;
    }

    public bool Receive(int count)
    {
        if (count <= 0)
        {
            return false;
        }
        quantityOnHand += count;
        
        StockMovement movement =
            new StockMovement(nextSeq, "Received", count);
        history.Add(movement);
        nextSeq++;

        return true;
    }

    public bool Release(int count)
    {
        if (count <= 0 || count > quantityOnHand)
        {
            return false;
        }
        quantityOnHand -= count;
        StockMovement movement =
            new StockMovement(nextSeq, "Released", count);
        history.Add(movement);
        nextSeq++;
        return true;
    }

    public string MovementLines()
    {
        string lines = "";

        for (int i = 0; i < history.Count; i++)
        {
            if (i > 0)
            {
                lines += Environment.NewLine;
            }
            lines += history[i].Describe();
        }
        return lines;
    }

    public virtual string Describe()
    {
        return String.Format(
           "{0} {1} ({2})",
           Sku, Name, Category());
    }

    public string ReportLine()
    {
        return String.Format(
            " {0,-7} {1,-21} {2,-10} {3,4} ${4,11:N2}",
            Sku, Name, Category(), QuantityOnHand, ExtendedValue());
    }

    public override string ToString()
    {
        return Describe();
    }

}