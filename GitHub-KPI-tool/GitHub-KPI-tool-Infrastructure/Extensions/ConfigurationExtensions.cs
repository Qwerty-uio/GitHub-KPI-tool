using GitHub_KPI_tool_Application.Abstraction.ApiClients;
using GitHub_KPI_tool_Application.Abstraction.Repositories;
using GitHub_KPI_tool_Infrastructure.ApiClients;
using GitHub_KPI_tool_Infrastructure.Contexts;
using GitHub_KPI_tool_Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Octokit;

namespace GitHub_KPI_tool_Infrastructure.Extensions;

public static class ConfigurationExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql("Host=localhost;Port=5438;Username=postgres;Password=12345678;Database=github");
        });
        
        // Database
        {
            services.AddScoped<IRepositoryRepository, RepositoryEntityRepository>();
            services.AddScoped<IUserRepository, UserEntityRepository>();
            services.AddScoped<IUserRepositoryActivityRepository, UserRepositoryActivityEntityRepository>();
        }
        
        // Octokit.net
        {
            services.AddSingleton<IGitHubClient, GitHubClient>((provider => new GitHubClient(new ProductHeaderValue("GitHub-KPI-Tool"))
            {
                Credentials = new Credentials("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")
            }));
            services.AddScoped<IGetRepository, GetRepository>();
            services.AddScoped<IGetPullRequests, GetPullRequests>();
            services.AddScoped<IGetCommits, GetCommits>();
            services.AddScoped<IGetIssues, GetIssues>();
        }

        return services;
    }
}