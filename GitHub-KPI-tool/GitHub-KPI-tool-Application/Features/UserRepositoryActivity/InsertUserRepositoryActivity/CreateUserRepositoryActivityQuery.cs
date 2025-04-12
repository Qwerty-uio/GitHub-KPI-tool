using MediatR;

namespace GitHub_KPI_tool_Application.Features.UserRepositoryActivity.InsertUserRepositoryActivity;

public record CreateUserRepositoryActivityQuery(int UserId, int RepositoryId): IRequest<CreateUserRepositoryActivityQueryResult>
{
    
}