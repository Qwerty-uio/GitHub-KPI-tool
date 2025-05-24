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

        return Calculator.CalculateMarks(rawMarks);
    }

    public IDictionary<string, double> CalculateRawMarkForCommits(List<GitHubCommitModel> commits)
    {
        var resultAddition = new Dictionary<string, double>();
        var resultDeletion = new Dictionary<string, double>();
        var resultTotal = new Dictionary<string, double>();
        var resultAmount = new Dictionary<string, double>();
        
        int sumAddition = 0;
        int sumDeletion = 0;
        int sumTotal = 0;
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

                sumAddition += commit.Stats.Additions;
                sumDeletion += commit.Stats.Deletions;
                sumTotal += commit.Stats.Total;
                amount++;
            }
        }

        var result = new Dictionary<string, double>();
        
        Calculator.Add(result, resultAddition,sumAddition);
        Calculator.Add(result, resultDeletion,sumDeletion);
        Calculator.Add(result, resultTotal,sumTotal);
        Calculator.Add(result, resultAmount,amount);

        foreach (var pair in resultAddition)
        {
            result[pair.Key] = Math.Sqrt(pair.Value/4);
        }
        
        return result;
    }
    
    
}