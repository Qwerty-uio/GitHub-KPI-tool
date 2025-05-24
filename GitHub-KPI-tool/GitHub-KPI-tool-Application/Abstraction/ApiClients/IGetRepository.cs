using Octokit;

namespace GitHub_KPI_tool_Application.Abstraction.ApiClients;

public interface IGetRepository : IBaseApiGitHub
{
    public Task<Repository> Get(string owner, string repositoryName);
}