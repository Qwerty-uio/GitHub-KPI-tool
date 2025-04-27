using GitHub_KPI_tool_Application.Abstraction.ApiClients;
using Octokit;

namespace GitHub_KPI_tool_Infrastructure.ApiClients;

public class GetCommits: IGetCommits
{
    private readonly IGitHubClient _client;

    public GetCommits(IGitHubClient client)
    {
        _client = client;
    }

    public async Task<IReadOnlyList<GitHubCommit>> Get(string owner, string repository)
    {
        var rawCommits = await _client.Repository.Commit.GetAll(owner, repository);
        var commits = new List<GitHubCommit>();
        foreach (var rawCommit in rawCommits)
        {
            commits.Add(await _client.Repository.Commit.Get(owner, repository, rawCommit.Sha));
        }
        
        return commits;
    }
}