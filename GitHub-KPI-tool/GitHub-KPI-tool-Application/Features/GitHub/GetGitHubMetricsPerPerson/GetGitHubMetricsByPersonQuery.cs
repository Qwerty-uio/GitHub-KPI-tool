using MediatR;

namespace GitHub_KPI_tool_Application.Features.GitHub.GetGitHubMetricsPerPerson;

public record GetGitHubMetricsByPersonQuery(): IRequest<GetGitHubMetricsByPersonQueryResult>;