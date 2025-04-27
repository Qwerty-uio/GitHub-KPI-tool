using Octokit;

namespace GitHub_KPI_tool_Application.Features.GitHub.GetCommits;

public class GetCommitsResult
{
    public IReadOnlyList<GitHubCommit> Commits { get; set; }
}