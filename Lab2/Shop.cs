// Manages the shop's stock items and reports.
public class Shop : IReportable
{
    private string name;
    private List<StockItem> items;

    public string Name
    {
        get { return name; }
    }

    public int Count
    {
        get { return items.Count; }
    }

    public Shop(string name)
    {
        this.name = name;
        items = new List<StockItem>();
    }

    public bool Add(StockItem item)
    {
        if (item == null || Find(item.Sku) != null)
        {
            return false;
        }

        items.Add(item);
        return true;
    }

    public StockItem Find(string sku)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Sku == sku)
            {
                return items[i];
            }
        }

        return null;
    }

    public decimal TotalValue()
    {
        decimal total = 0m;

        for (int i = 0; i < items.Count; i++)
        {
            total += items[i].ExtendedValue();
        }

        return total;
    }

    public decimal SaleValue()
    {
        decimal total = 0m;

        for (int i = 0; i < items.Count; i++)
        {
            StockItem item = items[i];

            if (item is IDiscountable d && d.IsOnSale)
            {
                total += (d.SalePrice() + item.HandlingFee())
                    * item.QuantityOnHand;
            }
            else
            {
                total += item.ExtendedValue();
            }
        }

        return total;
    }

    public int SignedCount()
    {
        int count = 0;

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] is IDiscountable)
            {
                count++;
            }
        }

        return count;
    }

    public int OnSaleCount()
    {
        int count = 0;

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] is IDiscountable d && d.IsOnSale)
            {
                count++;
            }
        }

        return count;
    }

    public void SortByValue()
    {
        for (int i = 0; i < items.Count - 1; i++)
        {
            int best = i;

            for (int j = i + 1; j < items.Count; j++)
            {
                if (Beats(items[j], items[best]))
                {
                    best = j;
                }
            }

            if (best != i)
            {
                StockItem hold = items[i];
                items[i] = items[best];
                items[best] = hold;
            }
        }
    }

    private static bool Beats(StockItem a, StockItem b)
    {
        if (a.ExtendedValue() != b.ExtendedValue())
        {
            return a.ExtendedValue() > b.ExtendedValue();
        }

        return string.Compare(
            a.Name,
            b.Name,
            StringComparison.Ordinal) < 0;
    }

    public string ReportLine()
    {
        return String.Format(
            "{0}: {1} items, ${2:N2} on hand",
            Name,
            Count,
            TotalValue());
    }

    public void PrintReport()
    {
        Console.WriteLine(new string('=', 60));
        Console.WriteLine(
            "  " + Name.ToUpper() + " : INVENTORY REPORT");
        Console.WriteLine(new string('=', 60));

        Console.WriteLine(String.Format(
            " {0,-7} {1,-21} {2,-10} {3,4} {4,11}",
            "SKU",
            "ITEM",
            "CATEGORY",
            "QTY",
            "VALUE"));

        Console.WriteLine(new string('-', 60));

        for (int i = 0; i < items.Count; i++)
        {
            Console.WriteLine(items[i].ReportLine());
        }

        Console.WriteLine(new string('-', 60));

        Console.WriteLine(String.Format(
            " {0,-46} {1,11}",
            "Records on file:",
            Count));

        Console.WriteLine(String.Format(
            " {0,-46}${1,11:N2}",
            "Total value on hand:",
            TotalValue()));

        Console.WriteLine(String.Format(
            " {0,-46}${1,11:N2}",
            "Value if every sale price were taken:",
            SaleValue()));

        Console.WriteLine(new string('=', 60));
    }
}