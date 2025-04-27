using GitHub_KPI_tool_Application.Abstraction.ApiClients;
using Octokit;

namespace GitHub_KPI_tool_Infrastructure.ApiClients;

public class GetPullRequests : IGetPullRequests
{
    private readonly IGitHubClient _client;

    public GetPullRequests(IGitHubClient client)
    {
        _client = client;
    }

    public async Task<IReadOnlyList<PullRequest>> Get(string owner, string repositoryName)
    {
        var rawPullRequests = await _client.PullRequest.GetAllForRepository(owner, repositoryName,new PullRequestRequest()
        {
            State = ItemStateFilter.All
        });
        
        var pullRequests = new List<PullRequest>();
        foreach (var rawPullRequest in rawPullRequests)
        {
            pullRequests.Add(await _client.PullRequest.Get(owner, repositoryName, rawPullRequest.Number));
        }
        
        return pullRequests;
    }
}