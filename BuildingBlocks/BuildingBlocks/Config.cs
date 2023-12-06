using BuildingBlocks.Application;
using BuildingBlocks.Infrastructure;
using BuildingBlocks.Infrastructure.Database.Transaction;
using BuildingBlocks.Integration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks;

public static class Config
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DatabaseConnection")));

        services.AddScoped<IScheduler, Scheduler>();
        services.AddScoped<IIntegrationEventPublisher, IntegrationEventPublisher>();
        services.AddScoped<IDbTransaction, DbTransaction>();
        services.AddTransient<IQueryBus, QueryBus>();
        services.AddTransient<ICommandBus, CommandBus>();
        return services;
    }
}