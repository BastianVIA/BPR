namespace Frontend.ServiceExtensions;

public static class ValidationSettings
{
    public static IServiceCollection AddValidationSettings(this IServiceCollection services, WebApplicationBuilder builder)
    {
        builder.Services.Configure<Config.ValidationSettings>(
            builder.Configuration.GetSection("ValidationSettings"));
        return services;
    }
}