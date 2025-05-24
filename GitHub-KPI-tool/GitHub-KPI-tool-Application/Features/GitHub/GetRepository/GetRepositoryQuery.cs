using MediatR;

namespace GitHub_KPI_tool_Application.Features.GitHub.GetRepository;

public record GetRepositoryQuery(string Owner, string Repository) : IRequest<GetRepositoryQueryResult>
{
}