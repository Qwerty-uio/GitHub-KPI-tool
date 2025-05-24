using GitHub_KPI_tool_Application.Models.PullRequest;
using Octokit;

namespace GitHub_KPI_tool_Application.Mappers;

public static class GithubPullRequestMapper
{
    public static GitHubPullRequestModel MapToModel(PullRequest pullRequest)
    {
        IReadOnlyList<string> assigneeNames =
            pullRequest?.Assignees?.Select(a => a.Login)?.ToList() ?? new List<string>();
        if (pullRequest?.Assignee != null)
        {
            assigneeNames.Append(pullRequest?.Assignee?.Login);
        }

        return new GitHubPullRequestModel(
            pullRequest.Number,
            pullRequest.Title,
            pullRequest.Body,
            pullRequest.User.Login,
            assigneeNames,
            pullRequest.State.StringValue == "open" ? State.Open : pullRequest.Merged ? State.Merged : State.Closed,
            pullRequest?.MergedBy?.Login,
            new Stats(pullRequest.Commits, pullRequest.Additions, pullRequest.Deletions, pullRequest.ChangedFiles));
    }

    public static List<GitHubPullRequestModel> MapToModels(List<PullRequest> pullRequests) =>
        pullRequests.Select(MapToModel).ToList();
}