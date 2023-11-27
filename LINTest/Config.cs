﻿using LINTest.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;

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


        services.AddSingleton<ConfigurationManager>();
        services.AddSingleton(stateManagerOptions);
        services.AddHostedService<LINTestBackgroundService>();
        
        services.AddSingleton<FileProcessor>();
        services.AddSingleton<CsvDataService>();
        services.AddSingleton<FileProcessingStateManager>();
        
        services.AddSingleton<FileProcessorOptions>(serviceProvider => 
            configuration.GetSection("LINTest:FileProcessor").Get<FileProcessorOptions>());

        services.AddSingleton<StateManagerOptions>(serviceProvider => 
            configuration.GetSection("LINTest:StateManager").Get<StateManagerOptions>());


        return services;
    }
}