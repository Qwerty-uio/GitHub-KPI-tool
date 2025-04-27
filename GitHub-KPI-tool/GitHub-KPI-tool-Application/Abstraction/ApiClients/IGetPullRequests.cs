using Octokit;

namespace GitHub_KPI_tool_Application.Abstraction.ApiClients;

public interface IGetPullRequests : IBaseApiGitHub
{
    public Task<IReadOnlyList<PullRequest>> Get(string owner, string repositoryName);
}