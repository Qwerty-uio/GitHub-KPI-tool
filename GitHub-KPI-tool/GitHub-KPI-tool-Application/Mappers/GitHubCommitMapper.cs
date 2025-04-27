using GitHub_KPI_tool_Application.Models;
using Octokit;

namespace GitHub_KPI_tool_Application.Mappers
{
    public static class GitHubCommitMapper
    {
        public static GitHubCommitModel MapToModel(GitHubCommit commit)
        {
            return new GitHubCommitModel(commit?.Author?.Login, commit.Sha, commit?.Commit?.Message);
        }

        public static List<GitHubCommitModel> MapToModels(List<GitHubCommit> commits) => commits.Select(MapToModel).ToList();
    }
}
