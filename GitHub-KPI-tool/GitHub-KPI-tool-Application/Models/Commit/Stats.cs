namespace GitHub_KPI_tool_Application.Models.Commit;

public class Stats
{
    public Stats(int additions, int deletions, int total)
    {
        Additions = additions;
        Deletions = deletions;
        Total = total;
    }

    public int Additions { get; set; }
    public int Deletions { get; set; }
    public int Total { get; set; }
}