using System.Data;
using System.Data.Common;
using BuildingBlocks.Application;
using BuildingBlocks.Infrastructure;
using BuildingBlocks.Infrastructure.Database.Transaction;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BuildingBlocks;

public static class Config
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DatabaseConnection")).EnableSensitiveDataLogging());

        services.AddScoped<IScheduler, Scheduler>();
        services.AddScoped<IDbTransaction, DbTransaction>();
        services.AddTransient<IQueryBus, QueryBus>();
        services.AddTransient<ICommandBus, CommandBus>();
        return services;
    }
}