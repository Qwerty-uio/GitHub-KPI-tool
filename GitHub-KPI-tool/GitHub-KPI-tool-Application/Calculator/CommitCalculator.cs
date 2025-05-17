using System.Runtime.InteropServices;
using GitHub_KPI_tool_Application.Abstraction.ApiClients;
using GitHub_KPI_tool_Application.Abstraction.Calculator;
using GitHub_KPI_tool_Application.Models;
using GitHub_KPI_tool_Application.Models.Commit;

namespace GitHub_KPI_tool_Application.Calculator;

public class CommitCalculator : ICommitCalculator
{
    
    public IDictionary<string, int> CalculateMarkForCommits(List<GitHubCommitModel> commits)
    {
        var rawMarks = CalculateRawMarkForCommits(commits);

        var sortedPairs = rawMarks.ToList();
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

    public IDictionary<string, double> CalculateRawMarkForCommits(List<GitHubCommitModel> commits)
    {
        var resultAddition = new Dictionary<string, double>();
        var resultDeletion = new Dictionary<string, double>();
        var resultTotal = new Dictionary<string, double>();
        var resultAmount = new Dictionary<string, double>();
        int sum = 0;
        int sumAdd = 0;
        int sumDel = 0;
        int amount = 0;

        foreach (var commit in commits)
        {
            if (commit.Author is not null)
            {
                if (!resultAddition.TryAdd(commit.Author, commit.Stats.Additions))
                {
                    resultAddition[commit.Author] += commit.Stats.Additions;
                }

                if (!resultDeletion.TryAdd(commit.Author, commit.Stats.Deletions))
                {
                    resultDeletion[commit.Author] += commit.Stats.Deletions;
                }

                if (!resultTotal.TryAdd(commit.Author, commit.Stats.Total))
                {
                    resultTotal[commit.Author] += commit.Stats.Total;
                }

                if (!resultAmount.TryAdd(commit.Author,1))
                {
                    resultAmount[commit.Author]++;
                }

                sumAdd += commit.Stats.Additions;
                sumDel += commit.Stats.Deletions;
                sum += commit.Stats.Total;
                amount++;
            }
        }

        var result = new Dictionary<string, double>();
        
        foreach (var key in resultAddition.Keys)
        {
            result.Add(key,
                Math.Sqrt((Math.Pow(resultAddition[key] * 100.0 / sumAdd, 2)
                           + Math.Pow(resultDeletion[key] * 100.0 / sumDel, 2)
                           + Math.Pow(resultTotal[key] * 100.0 / sum, 2)
                           + Math.Pow(resultAmount[key] * 100.0 / amount, 2)) / (4))
            );
        }
        
        return result;
    }
}