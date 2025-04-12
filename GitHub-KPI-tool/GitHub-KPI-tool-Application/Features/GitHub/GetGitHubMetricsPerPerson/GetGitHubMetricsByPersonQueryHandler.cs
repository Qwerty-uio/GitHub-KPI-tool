using MediatR;

namespace GitHub_KPI_tool_Application.Features.GitHub.GetGitHubMetricsPerPerson;

public class GetGitHubMetricsByPersonQueryHandler: IRequestHandler<GetGitHubMetricsByPersonQuery, GetGitHubMetricsByPersonQueryResult>
{
    public Task<GetGitHubMetricsByPersonQueryResult> Handle(GetGitHubMetricsByPersonQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new GetGitHubMetricsByPersonQueryResult());
    }
}