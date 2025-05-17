using GitHub_KPI_tool_Application.Abstraction.ApiClients;
using GitHub_KPI_tool_Application.Mappers;
using GitHub_KPI_tool_Application.Models.Issue;
using Octokit;

namespace GitHub_KPI_tool_Infrastructure.ApiClients;

public class GetIssues : IGetIssues
{
    private readonly IGitHubClient _client;

    public GetIssues(IGitHubClient client)
    {
        _client = client;
    }

    public async Task<List<GitHubIssueModel>> Get(string owner, string repository, CancellationToken cancellationToken = default)
    {
        var rawIssues = await _client.Issue.GetAllForRepository(owner, repository,new RepositoryIssueRequest()
        {
            State = ItemStateFilter.All
        });
        var issues = new List<Issue>();
        foreach (var rawIssue in rawIssues)
        {
            issues.Add(await _client.Issue.Get(owner, repository, rawIssue.Number));
        }
        
        return GitHubIssueMapper.MapToModels(issues);
    }

    public async Task<List<GitHubIssueModel>> GetByDate(string owner, string repository, DateTimeOffset? dateFrom, CancellationToken cancellationToken = default)
    {
        var rawIssues = await _client.Issue.GetAllForRepository(owner, repository,new RepositoryIssueRequest()
        {
            State = ItemStateFilter.All,
            
        });
        var issues = new List<Issue>();
        foreach (var rawIssue in rawIssues)
        {
            issues.Add(await _client.Issue.Get(owner, repository, rawIssue.Number));
        }
        
        return GitHubIssueMapper.MapToModels(issues);
    }
}