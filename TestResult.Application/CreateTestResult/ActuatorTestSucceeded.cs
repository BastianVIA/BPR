using BuildingBlocks.Infrastructure.Database.Transaction;
using BuildingBlocks.Integration;
using BuildingBlocks.Integration.Inbox;
using LINTest.Integration;

namespace TestResult.Application.CreateTestResult;

public class ActuatorTestSucceeded : IIntegrationEventListener<ActuatorTestSucceededIntegrationEvent>
{
    private readonly IInbox _inbox;
    private readonly IDbTransaction _dbTransaction;

    public ActuatorTestSucceeded(IInbox inbox, IDbTransaction dbTransaction)
    {
        _inbox = inbox;
        _dbTransaction = dbTransaction;
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
        await _inbox.Add(InboxMessage.From(testResultCommand, notification.Id));
        
        await _dbTransaction.CommitAsync(cancellationToken);
    }
}