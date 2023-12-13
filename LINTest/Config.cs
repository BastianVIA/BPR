using LINTest.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LINTest;
public static class Config
{
    public static IServiceCollection AddLINTestServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<FileProcessorOptions>(_ => 
            configuration.GetSection("LINTest:FileProcessor").Get<FileProcessorOptions>()!);
        
        services.AddSingleton<ConfigurationManager>();
        services.AddHostedService<LINTestBackgroundService>();
        services.AddSingleton<FileProcessor>();
        services.AddSingleton<CsvDataService>();
        services.AddSingleton<FileProcessingStateManager>();
        
        return services;
    }
}