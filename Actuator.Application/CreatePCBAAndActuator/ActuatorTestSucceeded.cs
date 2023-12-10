using BuildingBlocks.Infrastructure.Database.Transaction;
using BuildingBlocks.Integration;
using BuildingBlocks.Integration.Inbox;
using LINTest.Integration;
using TestResult.Application.CreateTestResult;

namespace Application.CreatePCBAAndActuator;

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
        var createPcbaAndActuatorCommand = CreatePCBAAndActuatorCommand.Create(
            notification.WorkOrderNumber, 
            notification.SerialNumber,
            notification.PCBAUid,
            notification.ArticleNumber,
            notification.ArticleName,
            notification.CommunicationProtocol,
            notification.CreatedTime);
        await _inbox.Add(InboxMessage.From(createPcbaAndActuatorCommand, notification.Id));
        await _dbTransaction.CommitAsync(cancellationToken);
    }
}