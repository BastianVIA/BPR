using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Registration;

public static class RegisterCommandAndQueryHandlers
{
    public static IServiceCollection AddCommandAndQueryHandlers(this IServiceCollection services, Assembly assembly)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
        return services;
    }
}