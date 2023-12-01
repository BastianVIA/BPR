using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Integration.Inbox;

public class InboxPublisherBackgroundService : BackgroundService
{
    private readonly ILogger<InboxPublisherBackgroundService> _logger;
    private readonly IServiceScopeFactory _scopeFactory;

    public InboxPublisherBackgroundService(ILogger<InboxPublisherBackgroundService> logger, IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var publisher = scope.ServiceProvider.GetRequiredService<InboxPublisher>();
                await publisher.PublishPendingAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                // We don't want the background service to stop while the application continues,
                // so catching and logging.
                // Should certainly have some extra checks for the reasons, to act on it.
                _logger.LogError(ex, ex.Message, null!);
            }

            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }
}