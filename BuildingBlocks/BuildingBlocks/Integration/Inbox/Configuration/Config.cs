using BuildingBlocks.Infrastructure.Database.Models;
using BuildingBlocks.Registration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Integration.Inbox.Configuration;

public static class Config
{
    public static IServiceCollection AddInbox(this IServiceCollection services)
    {
        services.AddCommandAndQueryHandlers(AssemblyReference.Assembly);
        services.AddScoped<IInbox, InboxRepository<InboxMessageModel>>();
        services.AddScoped<IFailingInbox, InboxRepository<FailingInboxMessageModel>>();
        services.AddScoped<InboxPublisher>();
        services.AddHostedService<InboxPublisherBackgroundService>();
        services.AddHostedService<FailingInboxPublisherBackgroundService>();
        return services;
    }
}