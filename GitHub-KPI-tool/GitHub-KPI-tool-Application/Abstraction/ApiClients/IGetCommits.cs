using GitHub_KPI_tool_Application.Models;
using GitHub_KPI_tool_Application.Models.Commit;
using Octokit;

namespace GitHub_KPI_tool_Application.Abstraction.ApiClients;

public interface IGetCommits : IBaseApiGitHub
{
    public Task<List<GitHubCommitModel>> Get(string owner, string repository, CancellationToken cancellationToken = default);
    
    public Task<List<GitHubCommitModel>> GetByDate(string owner, string repository, DateTimeOffset? dateFrom, DateTimeOffset? dateTo, CancellationToken cancellationToken = default);
}