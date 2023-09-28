using Backend.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LINTest;

public static class Config
{
    public static IServiceCollection AddLINTestServices(this IServiceCollection services)
    {
        // services.AddScoped<DataHandlingService>();
        services.AddHostedService<LINTestBackgroundService>();
        return services;
    }
}