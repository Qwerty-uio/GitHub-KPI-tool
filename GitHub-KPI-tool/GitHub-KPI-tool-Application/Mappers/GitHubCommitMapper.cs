using GitHub_KPI_tool_Application.Models;
using GitHub_KPI_tool_Application.Models.Commit;
using Octokit;

namespace GitHub_KPI_tool_Application.Mappers;

public static class GitHubCommitMapper
{
    public static GitHubCommitModel MapToModel(GitHubCommit commit)
    {
        return new GitHubCommitModel(commit?.Author?.Login, commit.Sha, commit?.Commit?.Message, new Stats(commit.Stats.Additions, commit.Stats.Deletions, commit.Stats.Total));
    }

    public static List<GitHubCommitModel> MapToModels(List<GitHubCommit> commits) => commits.Select(MapToModel).ToList();
}