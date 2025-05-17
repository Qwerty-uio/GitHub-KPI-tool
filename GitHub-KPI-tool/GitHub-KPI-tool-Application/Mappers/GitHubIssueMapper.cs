using GitHub_KPI_tool_Application.Models.Issue;
using Octokit;

namespace GitHub_KPI_tool_Application.Mappers;

public static class GitHubIssueMapper
{
    public static GitHubIssueModel MapToModel(Issue issue)
    {
        return new GitHubIssueModel(issue.Number, issue.Title, issue.Body, issue.User.Login,issue.CreatedAt, issue.UpdatedAt, issue.ClosedAt, issue.ClosedBy?.Login)
        {
            State = issue.ClosedAt is null ? State.Open : State.Closed,
        };
    }
    
    public static List<GitHubIssueModel> MapToModels(List<Issue> issues) => issues.Select(MapToModel).ToList();
}