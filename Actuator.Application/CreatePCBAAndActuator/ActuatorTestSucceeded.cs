﻿using BuildingBlocks.Infrastructure.Database.Transaction;
using BuildingBlocks.Integration;
using BuildingBlocks.Integration.Inbox;
using LINTest.Integration;

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
        var cmd = CreatePCBAAndActuatorCommand.Create(
            notification.WorkOrderNumber, 
            notification.SerialNumber,
            notification.PCBAUid,
            notification.ArticleNumber,
            notification.ArticleName,
            notification.CommunicationProtocol,
            notification.CreatedTime);
        await _inbox.Add(InboxMessage.From(cmd, notification.Id));
        await _transaction.CommitAsync(cancellationToken);
    }
}