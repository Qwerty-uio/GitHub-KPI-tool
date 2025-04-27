using Octokit;

namespace GitHub_KPI_tool_Application.Features.GitHub.GetIssues;

public class GetIssuesResult
{
    public IReadOnlyList<Issue> Issues { get; set; }
}