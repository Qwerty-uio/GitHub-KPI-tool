using GitHub_KPI_tool_Application.Abstraction.ApiClients;
using Octokit;

namespace GitHub_KPI_tool_Infrastructure.ApiClients;

public class GetRepository : IGetRepository
{
    private readonly IGitHubClient _client;

    public GetRepository(IGitHubClient client)
    {
        _client = client;
    }

    public async Task<Repository> Get(string owner, string repositoryName)
    {
        var repository = await _client.Repository.Get(owner, repositoryName);
        
        return repository;
    }
}