using Frontend.Networking;

namespace Frontend.ServiceExtensions;

public static class ValidationSettings
{
    public static async Task<IServiceCollection> AddValidationSettings(this IServiceCollection services)
    {

        var client = services.BuildServiceProvider().GetRequiredService<INetwork>();

        var configFromBackend = await client.GetConfiguration();

        services.AddScoped<Config.ValidationSettings>(serviceProvider =>
        {
            var settings = new Config.ValidationSettings();
            settings.WorkOrderNumberLength = configFromBackend.ValidationSettings.WorkOrderNumberLength;
            settings.SerialNumberMinLength = configFromBackend.ValidationSettings.SerialNumberMinLength;
            settings.SerialNumberMaxLength = configFromBackend.ValidationSettings.SerialNumberMaxLength;
            return settings;
        });
        
        return services;
    }
}