using BuildingBlocks.Infrastructure.Database.Transaction;
using BuildingBlocks.Integration;
using BuildingBlocks.Integration.Inbox;
using LINTest.Integration;

namespace TestResult.Application.CreateTestResult;

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
        var testResultCommand = CreateTestResultCommand.Create(
            notification.WorkOrderNumber,
            notification.SerialNumber,
            notification.Tester,
            notification.Bay,
            notification.MinServoPosition,
            notification.MaxServoPosition,
            notification.MinBuslinkPosition,
            notification.MaxBuslinkPosition,
            notification.ServoStroke,
            notification.CreatedTime);
        
        if (await _inbox.IdenticalMessageAlreadyExists(notification.Id, testResultCommand))
        {
            return;
        }
        await _inbox.Add(InboxMessage.Create(testResultCommand, notification.Id));
        
        await _transaction.CommitAsync(cancellationToken);
    }
}