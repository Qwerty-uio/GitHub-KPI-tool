using GitHub_KPI_tool_Application.Models.Issue;

namespace GitHub_KPI_tool_Application.Features.GitHub.GetIssues;

public class GetIssuesResult
{
    public List<GitHubIssueModel> Issues { get; set; }
}