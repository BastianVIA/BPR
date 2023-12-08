using LINTest.LinakDB;
using Microsoft.Extensions.DependencyInjection;

namespace TECHLineService;

public static class Config
{
    public static IServiceCollection AddTECHLineServices(this IServiceCollection services)
    {

        services.AddScoped<IPCBAService, PCBADAO>();

        return services;
    }
}