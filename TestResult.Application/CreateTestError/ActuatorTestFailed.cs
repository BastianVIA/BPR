using BuildingBlocks.Infrastructure.Database.Transaction;
using BuildingBlocks.Integration;
using BuildingBlocks.Integration.Inbox;
using LINTest.Integration;

namespace TestResult.Application.CreateTestError;

public class ActuatorTestFailed : IIntegrationEventListener<ActuatorTestFailedIntegrationEvent>
{
    private readonly IInbox _inbox;
    private readonly IDbTransaction _transaction;

    public ActuatorTestFailed(IInbox inbox, IDbTransaction transaction)
    {
        _inbox = inbox;
        _transaction = transaction;
    }

    public async Task Handle(ActuatorTestFailedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var testErrorCommand = CreateTestErrorCommand.Create(
            notification.WorkOrderNumber,
            notification.SerialNumber,
            notification.Tester,
            notification.Bay,
            notification.ErrorCode,
            notification.ErrorMessage,
            notification.TimeOccured);
        await _inbox.Add(InboxMessage.From(testErrorCommand, notification.Id));
        
        await _transaction.CommitAsync(cancellationToken);
    }
}