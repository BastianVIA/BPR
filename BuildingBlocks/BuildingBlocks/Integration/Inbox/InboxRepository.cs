using BuildingBlocks.Infrastructure;
using BuildingBlocks.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Integration.Inbox;

public class InboxRepository<TEntity> : BaseRepository<TEntity>, IFailingInbox
    where TEntity : BaseInboxMessageModel, new()
{
    public InboxRepository(ApplicationDbContext dbContext, IScheduler scheduler) : base(dbContext, scheduler)
    {
    }

    public async Task<InboxMessage> GetById(Guid id)
    {
        var message = await Query().FirstOrDefaultAsync(model => model.Id == id);
        if (message is null)
        {
            throw new KeyNotFoundException($"Could not fine InboxMessage for id {id}");
        }

        return ToDomain(message);
    }

    public async Task Add(InboxMessage inboxMessage)
    {
        await AddAsync(FromDomain(inboxMessage), inboxMessage.GetDomainEvents());
    }

    public async Task<IEnumerable<InboxMessage>> GetUnProcessedMessages()
    {
        try
        {
            var messages = await Query().AsNoTracking().Where(message => message.ProcessedDate == null)
                .ToListAsync();
            return ToDomain(messages);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task Update(InboxMessage inboxMessage)
    {
        var toUpdate = await Query().FirstAsync(model => model.Id == inboxMessage.Id);
        toUpdate.ProcessedDate = inboxMessage.ProcessedDate;
        toUpdate.FailedAttempts = inboxMessage.FailedAttempts;
        toUpdate.FailureReason = inboxMessage.FailureReason;
        toUpdate.IsFailing = inboxMessage.IsFailing;
        await UpdateAsync(toUpdate, inboxMessage.GetDomainEvents());
    }

    private TEntity FromDomain(InboxMessage inboxMessage)
    {
        return new TEntity()
        {
            Id = inboxMessage.Id,
            OccurredOn = inboxMessage.OccurredOn,
            IntegrationEventId = inboxMessage.IntegrationEventId,
            MessageType = inboxMessage.MessageType,
            Payload = inboxMessage.Payload,
            FailedAttempts = inboxMessage.FailedAttempts,
            ProcessedDate = inboxMessage.ProcessedDate,
            FailureReason = inboxMessage.FailureReason,
            IsFailing = inboxMessage.IsFailing
        };
    }

    private IEnumerable<InboxMessage> ToDomain(IEnumerable<TEntity> inboxMessageModels)
    {
        List<InboxMessage> domainMessages = new();
        foreach (var inboxMessageModel in inboxMessageModels)
        {
            domainMessages.Add(ToDomain(inboxMessageModel));
        }

        return domainMessages;
    }

    private InboxMessage ToDomain(TEntity inboxMessage)
    {
        return new InboxMessage(
            inboxMessage.Id,
            inboxMessage.OccurredOn,
            inboxMessage.IntegrationEventId,
            inboxMessage.MessageType,
            inboxMessage.Payload,
            inboxMessage.ProcessedDate,
            inboxMessage.FailedAttempts,
            inboxMessage.FailureReason,
            inboxMessage.IsFailing);
    }
}