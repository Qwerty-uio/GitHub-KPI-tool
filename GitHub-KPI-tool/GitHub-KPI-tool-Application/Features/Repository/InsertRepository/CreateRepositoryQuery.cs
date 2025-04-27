using MediatR;


namespace GitHub_KPI_tool_Application.Features.Repository.InsertRepository;

public record CreateRepositoryQuery(string Owner,string Name, string Description) : IRequest<CreateRepositoryQueryResult>
{
}