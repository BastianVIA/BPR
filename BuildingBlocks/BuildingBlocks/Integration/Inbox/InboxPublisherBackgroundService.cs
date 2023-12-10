using BuildingBlocks.Infrastructure.Database.Transaction;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Integration.Inbox;

public class InboxPublisherBackgroundService : BackgroundService
{
    private readonly ILogger<InboxPublisherBackgroundService> _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    private int intervalAsSeconds;

    public InboxPublisherBackgroundService(ILogger<InboxPublisherBackgroundService> logger,
        IServiceScopeFactory scopeFactory, IConfiguration configuration)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
        intervalAsSeconds = Convert.ToInt32(configuration.GetSection("BackgroundServices:InboxPublisherIntervalInSeconds")
            .Value);
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var publisher = scope.ServiceProvider.GetRequiredService<InboxPublisher>();
                var transaction = scope.ServiceProvider.GetRequiredService<IDbTransaction>();
                await publisher.PublishPendingAsync(transaction, cancellationToken);
            }
            catch (Exception ex)
            {
                // We don't want the background service to stop while the application continues,
                // so catching and logging.
                _logger.LogError(ex, ex.Message);
            }

            await Task.Delay(TimeSpan.FromSeconds(intervalAsSeconds), cancellationToken);
        }
    }
}