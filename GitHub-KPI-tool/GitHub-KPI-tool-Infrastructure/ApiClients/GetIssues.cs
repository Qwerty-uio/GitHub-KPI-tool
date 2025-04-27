using GitHub_KPI_tool_Application.Abstraction.ApiClients;
using Octokit;

namespace GitHub_KPI_tool_Infrastructure.ApiClients;

public class GetIssues : IGetIssues
{
    private readonly IGitHubClient _client;

    public GetIssues(IGitHubClient client)
    {
        _client = client;
    }

    public async Task<IReadOnlyList<Issue>> Get(string owner, string repository)
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
        
        return issues;
    }
}