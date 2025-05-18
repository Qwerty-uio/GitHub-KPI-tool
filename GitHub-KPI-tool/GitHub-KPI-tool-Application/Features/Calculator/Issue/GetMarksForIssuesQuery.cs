using MediatR;

namespace GitHub_KPI_tool_Application.Features.Calculator.Issue;

public record GetMarksForIssuesQuery(string Owner, string Repository, DateTimeOffset DateFrom): IRequest<GetMarksForIssuesResult>;