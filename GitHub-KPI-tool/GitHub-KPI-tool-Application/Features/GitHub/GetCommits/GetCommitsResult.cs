using GitHub_KPI_tool_Application.Models;
using GitHub_KPI_tool_Application.Models.Commit;
using Octokit;

namespace GitHub_KPI_tool_Application.Features.GitHub.GetCommits;

public class GetCommitsResult
{
    public IReadOnlyList<GitHubCommitModel> Commits { get; set; }
}