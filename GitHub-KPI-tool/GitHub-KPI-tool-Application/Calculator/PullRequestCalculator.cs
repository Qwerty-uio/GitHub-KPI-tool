using GitHub_KPI_tool_Application.Abstraction.Calculator;
using GitHub_KPI_tool_Application.Models.PullRequest;

namespace GitHub_KPI_tool_Application.Calculator;

public class PullRequestCalculator : IPullRequestCalculator
{
    public IDictionary<string, int> CalculateMarkForPullRequests(List<GitHubPullRequestModel> pullRequests)
    {
        var resultAddition = new Dictionary<string, double>();
        var resultDeletion = new Dictionary<string, double>();
        var resultCommits = new Dictionary<string, double>();
        var resultFiles = new Dictionary<string, double>();
        var resultChecked = new Dictionary<string, double>();
        double sumAdditions = 0;
        double sumDeletions = 0;
        double sumCommits = 0;
        double sumChangedFiles = 0;
        int sumChecked = 0;

        foreach (var pullRequest in pullRequests)
        {
            double modifier = 0;
            switch (pullRequest.State)
            {
                case State.Open:
                    modifier = 1;
                    break;
                case State.Closed:
                    modifier = 0.5;
                    break;
                case State.Merged:
                    modifier = 2;
                    if (!resultChecked.TryAdd(pullRequest.MergedBy, 1))
                    {
                        resultChecked[pullRequest.MergedBy]++;
                    }

                    sumChecked++;
                    break;
            }

            if (!resultAddition.TryAdd(pullRequest.User, pullRequest.Stats.Additions * modifier))
            {
                resultAddition[pullRequest.User] += pullRequest.Stats.Additions * modifier;
            }

            if (!resultDeletion.TryAdd(pullRequest.User, pullRequest.Stats.Deletions * modifier))
            {
                resultDeletion[pullRequest.User] += pullRequest.Stats.Deletions * modifier;
            }

            if (!resultCommits.TryAdd(pullRequest.User, pullRequest.Number))
            {
                resultCommits[pullRequest.User] += pullRequest.Stats.Commits * modifier;
            }

            if (!resultFiles.TryAdd(pullRequest.User, pullRequest.Number))
            {
                resultFiles[pullRequest.User] += pullRequest.Stats.ChangedFiles * modifier;
            }

            sumAdditions += pullRequest.Stats.Additions * modifier;
            sumDeletions += pullRequest.Stats.Deletions * modifier;
            sumCommits += pullRequest.Stats.Commits * modifier;
            sumChangedFiles += pullRequest.Stats.ChangedFiles * modifier;
        }

        var result = new Dictionary<string, double>();

        Calculator.Add(result, resultAddition, sumAdditions);
        Calculator.Add(result, resultDeletion, sumDeletions);
        Calculator.Add(result, resultCommits, sumCommits);
        Calculator.Add(result, resultChecked, sumChecked);
        Calculator.Add(result, resultFiles, sumChangedFiles);

        foreach (var pair in result)
        {
            result[pair.Key] = Math.Sqrt(pair.Value/5);
        }

        return Calculator.CalculateMarks(result);
    }
}