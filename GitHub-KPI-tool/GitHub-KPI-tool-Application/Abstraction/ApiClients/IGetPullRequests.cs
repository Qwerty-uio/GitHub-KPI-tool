using GitHub_KPI_tool_Application.Models.PullRequest;

namespace GitHub_KPI_tool_Application.Abstraction.ApiClients;

public interface IGetPullRequests : IBaseApiGitHub
{
    public Task<List<GitHubPullRequestModel>> Get(string owner, string repository, CancellationToken cancellationToken = default);
    
    public Task<List<GitHubPullRequestModel>> GetByDate(string owner, string repository, DateTimeOffset? dateFrom, CancellationToken cancellationToken = default);
}