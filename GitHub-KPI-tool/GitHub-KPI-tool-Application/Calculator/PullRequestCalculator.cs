using GitHub_KPI_tool_Application.Abstraction.Calculator;
using GitHub_KPI_tool_Application.Models.PullRequest;

namespace GitHub_KPI_tool_Application.Calculator;

public class PullRequestCalculator : IPullRequestCalculator
{

    public async Task<IDictionary<string, int>> CalculateMarkForPullRequests(List<GitHubPullRequestModel> pullRequests)
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

        foreach (var key in resultAddition.Keys)
        {
            result.Add(key,
                Math.Sqrt((Math.Pow(resultAddition[key] * 100.0 / sumAdditions, 2)
                           + Math.Pow(resultDeletion[key] * 100.0 / sumDeletions, 2)
                           + Math.Pow(resultCommits[key] * 100.0 / sumCommits, 2)
                           + Math.Pow(resultFiles[key] * 100.0 / sumChangedFiles, 2)
                           /*+ Math.Pow(resultChecked[key] * 100.0 / sumChecked, 2)*/) / (5))
            );
        }
        
        var sortedPairs = result.ToList();
        sortedPairs.Sort((x, y) => x.Value.CompareTo(y.Value));
        var differences = new List<KeyValuePair<int,double>>();
        for (int i = 1; i < sortedPairs.Count; i++)
        {
            differences.Add(KeyValuePair.Create(i, sortedPairs[i].Value-sortedPairs[i-1].Value));
        }
        differences.Sort((x, y) => x.Value.CompareTo(y.Value));
        differences.Reverse();
        differences = differences.GetRange(0, 4);

        var marks = new Dictionary<string, int>();
        int mark = 1;
        int counter = 1;
        foreach (var pair in sortedPairs)
        {
            marks.Add(pair.Key, mark);
            if (differences.Exists((x) => x.Key == counter))
            {
                mark++;
            }
            counter++;
        }
        
        return marks;
    }
}