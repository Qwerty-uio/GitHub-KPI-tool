using GitHub_KPI_tool_Application.Models.Issue;

namespace GitHub_KPI_tool_Application.Abstraction.Calculator;

public interface IIssueCalculator
{
    public IDictionary<string, int> CalculateMarkForIssues(List<GitHubIssueModel> issues);
}