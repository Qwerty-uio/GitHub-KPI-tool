namespace GitHub_KPI_tool_Application.Models.PullRequest;

public class Stats
{
    public Stats(int commits, int additions, int deletions, int changedFiles)
    {
        Commits = commits;
        Additions = additions;
        Deletions = deletions;
        ChangedFiles = changedFiles;
    }

    public int Commits { get; private set; }
    public int Additions { get; private set; }
    public int Deletions { get; private set; }
    public int ChangedFiles { get; private set; }
}