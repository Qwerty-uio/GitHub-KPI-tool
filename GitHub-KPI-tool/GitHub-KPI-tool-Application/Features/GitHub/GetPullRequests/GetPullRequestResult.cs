using Octokit;

namespace GitHub_KPI_tool_Application.Features.GitHub.GetPullRequests;

public class GetPullRequestResult
{
    public IReadOnlyList<PullRequest> PullRequests { get; set; }
}