using System.Diagnostics.CodeAnalysis;
using BuildingBlocks.Domain;
using BuildingBlocks.Infrastructure;
using BuildingBlocks.Infrastructure.Database;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Integration.Inbox;

public class Inbox : BaseRepository<InboxMessageModel>, IInbox
{
    public Inbox(ApplicationDbContext dbContext, IScheduler scheduler) : base(dbContext, scheduler)
    {
    }

    public async Task Add(InboxMessage inboxMessage)
    {
        await AddAsync(FromDomain(inboxMessage), new List<IDomainEvent>());
    }

    public async Task<IEnumerable<InboxMessage>> GetUnProcessedMessages()
    {
        var messages = await Query().AsNoTracking().Where(message => message.ProcessedDate == null)
            .OrderBy(message => message.OccurredOn)
            .ToListAsync();
        return ToDomain(messages);
    }

    public async Task Update(InboxMessage inboxMessage)
    {
        var toUpdate = await Query().FirstAsync(model => model.Id == inboxMessage.Id);
        toUpdate.ProcessedDate = inboxMessage.ProcessedDate;
        toUpdate.FailedAttempts = inboxMessage.FailedAttempts;
        toUpdate.FailureReason = inboxMessage.FailureReason;
        await UpdateAsync(toUpdate, new List<IDomainEvent>());    }

    private InboxMessageModel FromDomain(InboxMessage inboxMessage)
    {
        return new InboxMessageModel
        {
            Id = inboxMessage.Id,
            OccurredOn = inboxMessage.OccurredOn,
            IntegrationEventId = inboxMessage.IntegrationEventId,
            MessageType = inboxMessage.MessageType,
            Payload = inboxMessage.Payload,
            FailedAttempts = inboxMessage.FailedAttempts,
            ProcessedDate = inboxMessage.ProcessedDate,
            FailureReason = inboxMessage.FailureReason
        };
    }
    
    private IEnumerable<InboxMessage> ToDomain(IEnumerable<InboxMessageModel> inboxMessageModels)
    {
        List<InboxMessage> domainMessages = new();
        foreach (var inboxMessageModel in inboxMessageModels)
        {
            domainMessages.Add(ToDomain(inboxMessageModel));
        }

        return domainMessages;
    }

    private InboxMessage ToDomain(InboxMessageModel inboxMessage)
    {
        return new InboxMessage(
            inboxMessage.Id,
            inboxMessage.OccurredOn,
            inboxMessage.IntegrationEventId,
            inboxMessage.MessageType,
            inboxMessage.Payload,
            inboxMessage.ProcessedDate,
            inboxMessage.FailedAttempts,
            inboxMessage.FailureReason);
    }
}