using GitHub_KPI_tool_Application.Abstraction.ApiClients;
using GitHub_KPI_tool_Application.Mappers;
using GitHub_KPI_tool_Application.Models.Issue;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Octokit;

namespace GitHub_KPI_tool_Infrastructure.ApiClients;

public class GetIssues : IGetIssues
{
    private readonly IGitHubClient _client;
    private readonly IDistributedCache _distributedCache;

    public GetIssues(IGitHubClient client, IDistributedCache distributedCache)
    {
        _client = client;
        _distributedCache = distributedCache;
    }

    public async Task<List<GitHubIssueModel>> Get(string owner, string repository,
        CancellationToken cancellationToken = default)
    {
        var key = $"issues-{owner}/{repository}";
        var issues = new List<Issue>();

        try
        {
            var cachedIssues = await _distributedCache.GetStringAsync(key, cancellationToken);
            if (string.IsNullOrEmpty(cachedIssues))
            {
                var rawIssues = await _client.Issue.GetAllForRepository(owner, repository, new RepositoryIssueRequest()
                {
                    State = ItemStateFilter.All
                });
                foreach (var rawIssue in rawIssues)
                {
                    issues.Add(await _client.Issue.Get(owner, repository, rawIssue.Number));
                }

                var models = GitHubIssueMapper.MapToModels(issues);
                var options = new DistributedCacheEntryOptions()
                    { AbsoluteExpiration = DateTimeOffset.UtcNow.AddDays(1) };
                await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(models), options,
                    cancellationToken);
                return models;
            }

            return JsonConvert.DeserializeObject<IReadOnlyList<GitHubIssueModel>>(cachedIssues).ToList();
        }
        catch (Exception e)
        {
            throw;
        }
    }

    public async Task<List<GitHubIssueModel>> GetByDate(string owner, string repository, DateTimeOffset? dateFrom,
        CancellationToken cancellationToken = default)
    {
        var key = $"issues-{owner}/{repository}/{dateFrom}";
        var issues = new List<Issue>();

        try
        {
            var cachedIssues = await _distributedCache.GetStringAsync(key, cancellationToken);
            if (string.IsNullOrEmpty(cachedIssues))
            {
                var rawIssues = await _client.Issue.GetAllForRepository(owner, repository, new RepositoryIssueRequest()
                {
                    State = ItemStateFilter.All,
                });

                foreach (var rawIssue in rawIssues)
                {
                    issues.Add(await _client.Issue.Get(owner, repository, rawIssue.Number));
                }

                var models = GitHubIssueMapper.MapToModels(issues);
                var options = new DistributedCacheEntryOptions()
                    { AbsoluteExpiration = DateTimeOffset.UtcNow.AddDays(1) };
                await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(models), options,
                    cancellationToken);
                return models;
            }

            return JsonConvert.DeserializeObject<IReadOnlyList<GitHubIssueModel>>(cachedIssues).ToList();
        }
        catch (Exception e)
        {
            throw;
        } 
    }
}