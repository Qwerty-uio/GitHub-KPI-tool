using GitHub_KPI_tool_Application.Models.PullRequest;

namespace GitHub_KPI_tool_Application.Abstraction.Calculator;

public interface IPullRequestCalculator
{
    public Task<IDictionary<string, int>> CalculateMarkForPullRequests(List<GitHubPullRequestModel> pullRequests);
}