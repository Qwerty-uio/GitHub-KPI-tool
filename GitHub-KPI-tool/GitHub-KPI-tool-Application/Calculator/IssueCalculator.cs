using GitHub_KPI_tool_Application.Abstraction.Calculator;
using GitHub_KPI_tool_Application.Models.Issue;

namespace GitHub_KPI_tool_Application.Calculator;

public class IssueCalculator : IIssueCalculator
{
    public IDictionary<string, int> CalculateMarkForIssues(List<GitHubIssueModel> issues)
    {
        var resultCreated = new Dictionary<string, double>();
        var resultClosed = new Dictionary<string, double>();
        int sumCreated = 0;
        int sumClosed = 0;
        foreach (var issue in issues)
        {
            if (!resultCreated.TryAdd(issue.User, 1))
            {
                resultCreated[issue.User]++;
            }
            sumCreated++;

            if (issue.ClosedBy is not null)
            {
                if (!resultClosed.TryAdd(issue.ClosedBy, 1))
                {
                    resultClosed[issue.ClosedBy]++;
                }
                sumClosed++;
            }
        }
        
        var result = new Dictionary<string, double>();
        
        Calculator.Add(result, resultCreated, sumCreated);
        Calculator.Add(result, resultClosed, sumClosed);
        
        foreach (var pair in result)
        {
            result[pair.Key] = Math.Sqrt(pair.Value/2);
        }

        return Calculator.CalculateMarks(result);
    }
}