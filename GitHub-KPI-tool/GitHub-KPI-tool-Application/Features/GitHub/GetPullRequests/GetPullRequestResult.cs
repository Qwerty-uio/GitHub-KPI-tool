using GitHub_KPI_tool_Application.Models.PullRequest;
using Octokit;

namespace GitHub_KPI_tool_Application.Features.GitHub.GetPullRequests;

public class GetPullRequestResult
{
    public IReadOnlyList<GitHubPullRequestModel> PullRequests { get; set; }
}