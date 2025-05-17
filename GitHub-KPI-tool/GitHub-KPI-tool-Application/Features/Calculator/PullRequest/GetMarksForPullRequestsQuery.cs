using MediatR;
using Octokit.Internal;

namespace GitHub_KPI_tool_Application.Features.Calculator.PullRequest;

public record GetMarksForPullRequestsQuery(string Owner, string Repository, DateTimeOffset DateFrom): IRequest<GetMarksForPullRequestsResult>;