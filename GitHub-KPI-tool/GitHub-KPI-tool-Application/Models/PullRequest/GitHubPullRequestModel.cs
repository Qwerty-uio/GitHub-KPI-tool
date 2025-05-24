namespace GitHub_KPI_tool_Application.Models.PullRequest;

public class GitHubPullRequestModel
{
    public GitHubPullRequestModel(int number, string title, string body, string user, IReadOnlyList<string> assignees, State state, string? mergedBy, Stats stats)
    {
        Number = number;
        Title = title;
        Body = body;
        User = user;
        Assignees = assignees;
        State = state;
        MergedBy = mergedBy;
        Stats = stats;
    }

    public int Number { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public string User { get; set; }
    public IReadOnlyList<string> Assignees { get; set; }
    public State State { get; set; }
    public string? MergedBy { get; set; }
    public Stats Stats { get; set; }
}