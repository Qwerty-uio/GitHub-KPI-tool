using GitHub_KPI_tool_Application.Models.Commit;

namespace GitHub_KPI_tool_Application.Abstraction.Calculator;

public interface ICommitCalculator
{
    public IDictionary<string, int> CalculateMarkForCommits(List<GitHubCommitModel> commits);
    public IDictionary<string, double> CalculateRawMarkForCommits(List<GitHubCommitModel> commits);
}