using System.Transactions;

// Records a successful inventory movement.
public class StockMovement
{
    private int seq;
    private string kind;
    private int count;

    public int Seq
    {
        get { return seq; }
    }

    public string Kind
    {
        get { return kind; }
    }

    public int Count
    {
        get { return count; }
    }
    public StockMovement(int seq, string kind, int count)
    {
        this.seq = seq;
        this.kind = kind;
        this.count = count;
    }

    public string Describe()
    {
        return String.Format(" move {0}: {1} {2}", Seq, Kind, Count);
    }

}