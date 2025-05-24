using GitHub_KPI_tool_Application.Abstraction.ApiClients;
using GitHub_KPI_tool_Application.Mappers;
using GitHub_KPI_tool_Application.Models.PullRequest;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Octokit;

namespace GitHub_KPI_tool_Infrastructure.ApiClients;

public class GetPullRequests : IGetPullRequests
{
    private readonly IGitHubClient _client;
    private readonly IDistributedCache _distributedCache;

    public GetPullRequests(IGitHubClient client, IDistributedCache distributedCache)
    {
        _client = client;
        _distributedCache = distributedCache;
    }

    public async Task<List<GitHubPullRequestModel>> Get(string owner, string repository,
        CancellationToken cancellationToken = default)
    {
        var key = $"pullRequests-{owner}/{repository}";

        var pullRequests = new List<PullRequest>();

        try
        {
            var cachedPullRequests = await _distributedCache.GetStringAsync(key, cancellationToken);

            if (string.IsNullOrEmpty(cachedPullRequests))
            {
                var rawPullRequests = await _client.PullRequest.GetAllForRepository(owner, repository,
                    new PullRequestRequest()
                    {
                        State = ItemStateFilter.All
                    });

                foreach (var rawPullRequest in rawPullRequests)
                {
                    pullRequests.Add(await _client.PullRequest.Get(owner, repository, rawPullRequest.Number));
                }

                var models = GithubPullRequestMapper.MapToModels(pullRequests);
                var options = new DistributedCacheEntryOptions()
                    { AbsoluteExpiration = DateTimeOffset.UtcNow.AddDays(1) };

                await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(models), options,
                    cancellationToken);

                return models;
            }

            return JsonConvert.DeserializeObject<IReadOnlyList<GitHubPullRequestModel>>(cachedPullRequests).ToList();
        }
        catch (Exception e)
        {
            throw;
        }
    }


    public async Task<List<GitHubPullRequestModel>> GetByDate(string owner, string repository,
        DateTimeOffset? dateFrom, CancellationToken cancellationToken = default)
    {
        var key = $"pullRequests-{owner}/{repository}/{dateFrom}";

        var pullRequests = new List<PullRequest>();

        try
        {
            var cachedPullRequests = await _distributedCache.GetStringAsync(key, cancellationToken);

            if (string.IsNullOrEmpty(cachedPullRequests))
            {
                var rawPullRequests = await _client.PullRequest.GetAllForRepository(owner, repository,
                    new PullRequestRequest()
                    {
                        State = ItemStateFilter.All,
                    });

                foreach (var rawPullRequest in rawPullRequests)
                {
                    if (rawPullRequest.CreatedAt >= dateFrom)
                    {
                        pullRequests.Add(await _client.PullRequest.Get(owner, repository, rawPullRequest.Number));
                    }
                }

                var models = GithubPullRequestMapper.MapToModels(pullRequests);
                var options = new DistributedCacheEntryOptions()
                    { AbsoluteExpiration = DateTimeOffset.UtcNow.AddDays(1) };

                await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(models), options,
                    cancellationToken);

                return models;
            }

            return JsonConvert.DeserializeObject<IReadOnlyList<GitHubPullRequestModel>>(cachedPullRequests).ToList();
        }
        catch (Exception e)
        {
            throw;
        }
    }
}