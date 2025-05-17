using GitHub_KPI_tool_Application.Abstraction.ApiClients;
using GitHub_KPI_tool_Application.Abstraction.Calculator;
using GitHub_KPI_tool_Application.Abstraction.Repositories;
using GitHub_KPI_tool_Application.Calculator;
using GitHub_KPI_tool_Infrastructure.ApiClients;
using GitHub_KPI_tool_Infrastructure.Contexts;
using GitHub_KPI_tool_Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Octokit;

namespace GitHub_KPI_tool_Infrastructure.Extensions;

public static class ConfigurationExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfigurationRoot configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            var connection = configuration.GetConnectionString("Postgres");

            ArgumentNullException.ThrowIfNull(connection);

            options.UseNpgsql(connection);
        });

        //Redis
        services.AddStackExchangeRedisCache(redisOpntions =>
        {
            var connection = configuration.GetConnectionString("Redis");

            ArgumentNullException.ThrowIfNull(connection);

            redisOpntions.Configuration = connection;
        });

        // Database
        {
            services.AddScoped<IRepositoryRepository, RepositoryEntityRepository>();
            services.AddScoped<IUserRepository, UserEntityRepository>();
            services.AddScoped<IUserRepositoryActivityRepository, UserRepositoryActivityEntityRepository>();
        }
        
        // Octokit.net
        {
            services.AddSingleton<IGitHubClient, GitHubClient>(provider =>
            new GitHubClient(new ProductHeaderValue("GitHub-KPI-Tool"))
            {
                Credentials = new Credentials(Environment.GetEnvironmentVariable("KEY"))
            });
            

            services.AddScoped<IGetRepository, GetRepository>();
            services.AddScoped<IGetPullRequests, GetPullRequests>();
            services.AddScoped<IGetCommits, GetCommits>();
            services.AddScoped<IGetIssues, GetIssues>();
            services.AddScoped<ICommitCalculator, CommitCalculator>();
            services.AddScoped<IPullRequestCalculator, PullRequestCalculator>();
        }

        return services;
    }
}