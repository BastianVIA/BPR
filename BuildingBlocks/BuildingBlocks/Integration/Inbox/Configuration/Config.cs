using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Integration.Inbox.Configuration;

public static class Config
{
    public static IServiceCollection AddInbox(this IServiceCollection services)
    {
        services.AddScoped<IInbox, Inbox>();        
        services.AddScoped<InboxPublisher>();
        services.AddHostedService<InboxPublisherBackgroundService>();
        return services;
    }
}