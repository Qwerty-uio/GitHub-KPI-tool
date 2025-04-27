using Octokit;

namespace GitHub_KPI_tool_Application.Abstraction.ApiClients;

public interface IGetCommits : IBaseApiGitHub
{
    public Task<IReadOnlyList<GitHubCommit>> Get(string owner, string repository);
}