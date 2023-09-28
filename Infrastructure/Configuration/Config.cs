using Application;
using BuildingBlocks.Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;
using BuildingBlocks.Registration;
using Domain.Repositories;

namespace Infrastructure.Configuration;

public static class Config
{
    public static IServiceCollection AddActuatorServices(this IServiceCollection services)
    {
        services.AddCommandAndQueryHandlers(AssemblyReference.Assembly);
        services.AddScoped<IActuatorRepository, ActuatorRepository>();
        
        services.AddEntityDbSet<ActuatorModel>(entity =>
        {
            entity.HasKey(a => new {a.WorkOrderNumber, a.SerialNumber});
        });
        return services;
    }
}