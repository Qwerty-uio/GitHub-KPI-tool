using GitHub_KPI_tool_Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace GitHub_KPI_tool_Infrastructure.Contexts;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<RepositoryEntity> Repositories { get; set; }
    public DbSet<UserRepositoryActivityEntity> UserRepositoryActivities { get; set; }
}