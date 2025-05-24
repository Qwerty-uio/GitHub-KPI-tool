using MediatR;

namespace GitHub_KPI_tool_Application.Features.GitHub.GetIssues;

public record GetIssuesQuery(string Owner, string Repository) : IRequest<GetIssuesResult>
{
    public DateTimeOffset? DateFrom { get; set; }
}