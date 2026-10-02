// Writes shelf counts to a file and safely closes the log.
public class CountLog : IDisposable
{
    private StreamWriter writer;
    private string path;
    private int count;
    private bool isClosed;

    public string Path
    {
        get { return path; }
    }

    public int Count
    {
        get { return count; }
    }

    public bool IsClosed
    {
        get { return isClosed; }
    }

    public CountLog(string path)
    {
        this.path = path;
        writer = new StreamWriter(path);
        writer.WriteLine("LOG OPENED");
        count = 0;
        isClosed = false;
    }

    public void Write(ShelfCount r)
    {
        count++;

        writer.WriteLine(String.Format(
            "{0,3}  {1}",
            Count,
            r));
    }

    public void Dispose()
    {
        if (isClosed)
        {
            return;
        }

        try
        {
            writer.WriteLine(String.Format(
                "LOG CLOSED, {0} lines written",
                Count));
        }
        catch
        {
        }

        try
        {
            writer.Close();
        }
        catch
        {
        }

        isClosed = true;
    }
}