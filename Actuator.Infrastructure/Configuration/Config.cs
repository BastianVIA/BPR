using Application;
using Microsoft.Extensions.DependencyInjection;
using BuildingBlocks.Registration;
using Domain.RepositoryInterfaces;

namespace Infrastructure.Configuration;

public static class Config
{
    public static IServiceCollection AddActuatorServices(this IServiceCollection services)
    {
        services.AddCommandAndQueryHandlers(AssemblyReference.Assembly);
        services.AddScoped<IActuatorRepository, ActuatorRepository>();
        services.AddScoped<IPCBARepository, PCBARepository>();
        services.AddScoped<IActuatorPCBAHistoryRepository, ActuatorPCBAHistoryRepository>();
        
        return services;
    }
}