using MediatR;

namespace GitHub_KPI_tool_Application.Features.User.InsertUser;

public record CreateUserQuery(string UserName, string Email) : IRequest<CreateUserQueryResult>
{
}
