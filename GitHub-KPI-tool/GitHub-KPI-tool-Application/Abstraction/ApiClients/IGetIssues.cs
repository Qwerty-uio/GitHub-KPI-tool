using Octokit;

namespace GitHub_KPI_tool_Application.Abstraction.ApiClients;

public interface IGetIssues : IBaseApiGitHub
{
    public Task<IReadOnlyList<Issue>> Get(string owner, string repository);
}