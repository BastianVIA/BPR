using BuildingBlocks.Registration;
using Microsoft.Extensions.DependencyInjection;
using TestResult.Application;
using TestResult.Domain.Repositories;

namespace TestResult.Infrastructure.Configuration;

public static class Config
{
    public static IServiceCollection AddTestResultServices(this IServiceCollection services)
    {
        services.AddCommandAndQueryHandlers(AssemblyReference.Assembly);
        services.AddScoped<ITestResultRepository, TestResultRepository>();
        services.AddScoped<ITestErrorRepository, TestErrorRepository>();
        return services;
    }
}