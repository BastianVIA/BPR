using BuildingBlocks.Infrastructure.Database.Transaction;
using BuildingBlocks.Integration;
using BuildingBlocks.Integration.Inbox;
using LINTest.Integration;

namespace Application.CreateOrUpdateActuator;

public class ActuatorTestSucceeded : IIntegrationEventListener<ActuatorTestSucceededIntegrationEvent>
{
    private readonly IInbox _inbox;
    private readonly IDbTransaction _transaction;

    public ActuatorTestSucceeded(IInbox inbox, IDbTransaction transaction)
    {
        _inbox = inbox;
        _transaction = transaction;
    }

    public async Task Handle(ActuatorTestSucceededIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var cmd = CreateOrUpdateActuatorCommand.Create(notification.WorkOrderNumber, notification.SerailNumber,
            notification.PCBAUid);
        await _inbox.Add(InboxMessage.From(cmd, notification.Id));
        await _transaction.CommitAsync(cancellationToken);
    }
}