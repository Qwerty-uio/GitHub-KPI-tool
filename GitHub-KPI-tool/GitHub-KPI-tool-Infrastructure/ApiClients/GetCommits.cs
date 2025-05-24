using GitHub_KPI_tool_Application.Abstraction.ApiClients;
using GitHub_KPI_tool_Application.Mappers;
using GitHub_KPI_tool_Application.Models;
using GitHub_KPI_tool_Application.Models.Commit;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Octokit;

namespace GitHub_KPI_tool_Infrastructure.ApiClients;

public class GetCommits : IGetCommits
{
    private readonly IGitHubClient _client;
    private readonly IDistributedCache _distributedCache;

    public GetCommits(IGitHubClient client, IDistributedCache distributedCache)
    {
        _client = client;
        _distributedCache = distributedCache;
    }

    public async Task<List<GitHubCommitModel>> Get(string owner, string repository,
        CancellationToken cancellationToken = default)
    {
        var key = $"commits-{owner}/{repository}";

        var commits = new List<GitHubCommit>();

        try
        {
            var cachedCommits = await _distributedCache.GetStringAsync(key, cancellationToken);

            if (string.IsNullOrEmpty(cachedCommits))
            {
                var rawCommits = await _client.Repository.Commit.GetAll(owner, repository);

                if (!rawCommits.Any())
                {
                    return new List<GitHubCommitModel>();
                }

                foreach (var rawCommit in rawCommits)
                {
                    commits.Add(await _client.Repository.Commit.Get(owner, repository, rawCommit.Sha));
                }

                var models = GitHubCommitMapper.MapToModels(commits);

                var options = new DistributedCacheEntryOptions()
                    { AbsoluteExpiration = DateTimeOffset.UtcNow.AddDays(1) };

                await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(models), options,
                    cancellationToken);

                return models;
            }

            return JsonConvert.DeserializeObject<IReadOnlyList<GitHubCommitModel>>(cachedCommits).ToList();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<List<GitHubCommitModel>> GetByDate(string owner, string repository, DateTimeOffset? dateFrom,
        DateTimeOffset? dateTo,
        CancellationToken cancellationToken = default)
    {
        var key = $"commits-{owner}/{repository}/{dateFrom:yyyyMMdd}-{dateTo:yyyyMMdd}";

        var commits = new List<GitHubCommit>();

        try
        {
            var cachedCommits = await _distributedCache.GetStringAsync(key, cancellationToken);

            if (string.IsNullOrEmpty(cachedCommits))
            {
                var rawCommits = await _client.Repository.Commit.GetAll(owner, repository,
                    new CommitRequest()
                    {
                        Since = dateFrom,
                        Until = dateTo
                    });

                if (!rawCommits.Any())
                {
                    return new List<GitHubCommitModel>();
                }

                foreach (var rawCommit in rawCommits)
                {
                    commits.Add(await _client.Repository.Commit.Get(owner, repository, rawCommit.Sha));
                }

                var models = GitHubCommitMapper.MapToModels(commits);

                var options = new DistributedCacheEntryOptions()
                    { AbsoluteExpiration = DateTimeOffset.UtcNow.AddDays(1) };

                await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(models), options,
                    cancellationToken);

                return models;
            }

            return JsonConvert.DeserializeObject<IReadOnlyList<GitHubCommitModel>>(cachedCommits).ToList();
        }
        catch (Exception e)
        {
            throw;
        }
    }
}