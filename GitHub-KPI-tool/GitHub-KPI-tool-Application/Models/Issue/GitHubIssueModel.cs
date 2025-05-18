namespace GitHub_KPI_tool_Application.Models.Issue;

public class GitHubIssueModel
{
    public GitHubIssueModel(int number, string? title, string? body, string? user, DateTimeOffset? createdAt, DateTimeOffset? updatedAt, DateTimeOffset? closedAt, string? closedBy)
    {
        Number = number;
        Title = title;
        Body = body;
        User = user;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        ClosedAt = closedAt;
        ClosedBy = closedBy;
    }

    public int Number { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public string User { get; set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? ClosedAt { get; set; }
    public string? ClosedBy { get; set; }
    public State? State { get; set; }
}