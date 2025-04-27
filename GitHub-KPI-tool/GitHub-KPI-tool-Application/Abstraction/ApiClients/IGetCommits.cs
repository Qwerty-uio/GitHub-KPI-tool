using GitHub_KPI_tool_Application.Models;
using Octokit;

namespace GitHub_KPI_tool_Application.Abstraction.ApiClients;

public interface IGetCommits : IBaseApiGitHub
{
    public Task<List<GitHubCommitModel>> Get(string owner, string repository, CancellationToken cancellationToken = default);
}