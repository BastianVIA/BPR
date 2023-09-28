using BuildingBlocks.Application;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks;

public static class Config
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddTransient<IQueryBus, QueryBus>();
        return services;
    }
}