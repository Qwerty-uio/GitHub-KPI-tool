using MediatR;

namespace GitHub_KPI_tool_Application.Features.GitHub.GetPullRequests;

public record GetPullRequestQuery(string Owner, string Repository) : IRequest<GetPullRequestResult>;