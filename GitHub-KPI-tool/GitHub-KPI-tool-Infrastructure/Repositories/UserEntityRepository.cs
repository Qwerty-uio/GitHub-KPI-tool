using GitHub_KPI_tool_Application.Abstraction.Repositories;
using GitHub_KPI_tool_Application.Entities;
using GitHub_KPI_tool_Infrastructure.Contexts;

namespace GitHub_KPI_tool_Infrastructure.Repositories;

public class UserEntityRepository(AppDbContext dbContext) : BaseRepository<UserEntity>(dbContext), IUserRepository
{
    
}