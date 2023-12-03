﻿using Frontend.Networking;

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
            settings.PCBAUidLenght = configFromBackend.ValidationSettings.PcbaUidLength;
            settings.WorkOrderNumberLength = configFromBackend.ValidationSettings.WorkOrderNumberLength;
            settings.SerialNumberMinLength = configFromBackend.ValidationSettings.SerialNumberMinLength;
            settings.SerialNumberMaxLength = configFromBackend.ValidationSettings.SerialNumberMaxLength;
            settings.ItemNumberLength = configFromBackend.ValidationSettings.ItemNumberLength;
            settings.ManufacturerNumberLength = configFromBackend.ValidationSettings.ManufacturerNumberLength;
            settings.ProductionDateCodeLenght = configFromBackend.ValidationSettings.ProductionDateCodeLenght;
            return settings;
        });
        return services;
    }
}