using BuildingBlocks.Infrastructure.Database.Transaction;
using BuildingBlocks.Integration;
using BuildingBlocks.Integration.Inbox;
using LINTest.Integration;
using TestResult.Application.CreateTestResult;

namespace Application.CreatePCBAAndActuator;

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
        var createPcbaAndActuatorCommand = CreatePCBAAndActuatorCommand.Create(
            notification.WorkOrderNumber, 
            notification.SerialNumber,
            notification.PCBAUid,
            notification.ArticleNumber,
            notification.ArticleName,
            notification.CommunicationProtocol,
            notification.CreatedTime);
        await _inbox.Add(InboxMessage.From(createPcbaAndActuatorCommand, notification.Id));

        var testResultCommand = CreateTestResultCommand.Create(
            notification.WorkOrderNumber,
            notification.SerialNumber,
            notification.Tester,
            notification.Bay,
            notification.MinServoPosition,
            notification.MaxServoPosition,
            notification.MinBuslinkPosition,
            notification.MaxBuslinkPosition);
        await _inbox.Add(InboxMessage.From(testResultCommand, notification.Id));
        
        await _transaction.CommitAsync(cancellationToken);
    }
}