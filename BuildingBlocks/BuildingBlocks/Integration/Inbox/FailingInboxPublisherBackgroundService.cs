﻿using BuildingBlocks.Infrastructure.Database.Transaction;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Integration.Inbox;

public class FailingInboxPublisherBackgroundService : BackgroundService
{
    private readonly ILogger<FailingInboxPublisherBackgroundService> _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    private int intervalAsHours;

    public FailingInboxPublisherBackgroundService(ILogger<FailingInboxPublisherBackgroundService> logger,
        IServiceScopeFactory scopeFactory, IConfiguration configuration)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
        intervalAsHours = Convert.ToInt32(configuration.GetSection("Inbox:InboxPublisherIntervalInSeconds")
            .Value);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var publisher = scope.ServiceProvider.GetRequiredService<InboxPublisher>();
                var transaction = scope.ServiceProvider.GetRequiredService<IDbTransaction>();
                await publisher.PublishFailingAsync(transaction, stoppingToken);
            }
            catch (Exception ex)
            {
                // We don't want the background service to stop while the application continues,
                // so catching and logging.
                _logger.LogError(ex, ex.Message);
            }

            await Task.Delay(TimeSpan.FromHours(intervalAsHours), stoppingToken);
        }
    }
}