using BuildingBlocks.Infrastructure.Database.Transaction;
using BuildingBlocks.Integration.Inbox.Serialization;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Integration.Inbox;

public class InboxPublisher
{
    private readonly IInbox _inbox;
    private readonly ISender _sender;
    private readonly ILogger<InboxPublisher> _logger;

    public InboxPublisher(IInbox inbox, ISender sender, ILogger<InboxPublisher> logger)
    {
        _inbox = inbox;
        _sender = sender;
        _logger = logger;
    }


    internal async Task PublishPendingAsync(CancellationToken cancellationToken)
    {
        var serializer = new InboxMessageSerializer();
        var messages = await _inbox.GetUnProcessedMessages();

        foreach (var message in messages)
        {
            try
            {
                var cmd = serializer.Deserialize(message.Payload, message.MessageType);
                if (cmd is not null)
                {
                    try
                    {
                        await _sender.Send(cmd, cancellationToken);
                        message.Processed();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "Processing InboxMessage {Id} failed", message.Id);
                        message.Failed("Processing Message Failed");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Deserialize InboxMessage {Id} failed", message.Id);
                message.Failed("Deserialize Message Failed");
            }
        }

        await _inbox.Processed(messages);
    }
}