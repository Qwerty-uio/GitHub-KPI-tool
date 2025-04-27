using GitHub_KPI_tool_Application.Abstraction.Repositories;
using GitHub_KPI_tool_Infrastructure.Contexts;
using GitHub_KPI_tool_Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GitHub_KPI_tool_Infrastructure.Extensions;

public static class ConfigurationExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql("Host=localhost;Port=5438;Username=postgres;Password=12345678;Database=github");
        });

        services.AddScoped<IRepositoryRepository, RepositoryEntityRepository>();
        services.AddScoped<IUserRepository, UserEntityRepository>();
        services.AddScoped<IUserRepositoryActivityRepository, UserRepositoryActivityEntityRepository>();
        
        return services;
    }
}