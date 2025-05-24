using MediatR;

namespace GitHub_KPI_tool_Application.Features.GitHub.GetCommits;

public record GetCommitsQuery(string Owner, string Repository) : IRequest<GetCommitsResult>
{
    public DateTimeOffset? DateFrom { get; set; }
    public DateTimeOffset? DateTo { get; set; }
}