using Microsoft.Extensions.DependencyInjection;

namespace GitHub_KPI_tool_Application;

public static class DependencyRegistrations
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddSwaggerGen();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<IAppAssemblyMarker>());
        return services;
    }
}