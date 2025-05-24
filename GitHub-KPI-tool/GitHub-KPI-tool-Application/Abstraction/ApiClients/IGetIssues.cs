using GitHub_KPI_tool_Application.Models.Issue;
using Octokit;

namespace GitHub_KPI_tool_Application.Abstraction.ApiClients;

public interface IGetIssues : IBaseApiGitHub
{
    public Task<List<GitHubIssueModel>> Get(string owner, string repository, CancellationToken cancellationToken = default);
    public Task<List<GitHubIssueModel>> GetByDate(string owner, string repository, DateTimeOffset? dateFrom, CancellationToken cancellationToken = default);
}