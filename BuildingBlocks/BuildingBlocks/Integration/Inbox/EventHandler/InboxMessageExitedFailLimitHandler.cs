using BuildingBlocks.Domain;
using BuildingBlocks.Infrastructure.Database.Transaction;
using BuildingBlocks.Integration.Inbox.Events;

namespace BuildingBlocks.Integration.Inbox.EventHandler;

public class InboxMessageExitedFailLimitHandler : IDomainEventListener<InboxMessageExitedFailLimitDomainEvent>
{
    private readonly IFailingInbox _failingInbox;
    private readonly IInbox _inbox;
    private readonly IDbTransaction _transaction;
    public InboxMessageExitedFailLimitHandler(IFailingInbox failingInbox, IInbox inbox, IDbTransaction transaction)
    {
        _failingInbox = failingInbox;
        _inbox = inbox;
        _transaction = transaction;
    }

    public async Task Handle(InboxMessageExitedFailLimitDomainEvent notification, CancellationToken cancellationToken)
    {
        var failedMsg = await _inbox.GetById(notification.Id);
        failedMsg.MarkFailedBackToAsNotProcessed();
        await _failingInbox.Add(failedMsg);
        await _transaction.CommitAsync(cancellationToken);
    }
}