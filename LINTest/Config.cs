using LINTest.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LINTest;

public static class Config
{
    public static IServiceCollection AddLINTestServices(this IServiceCollection services, IConfiguration configuration)
    {
        var fileProcessorOptions = new FileProcessorOptions();
        configuration.GetSection("FileProcessor").Bind(fileProcessorOptions);
        
        services.AddSingleton(fileProcessorOptions);

        var stateManagerOptions = new StateManagerOptions();
        configuration.GetSection("StateManager").Bind(stateManagerOptions);
        services.AddSingleton(stateManagerOptions);

        services.AddHostedService<LINTestBackgroundService>();

        return services;
    }

}
