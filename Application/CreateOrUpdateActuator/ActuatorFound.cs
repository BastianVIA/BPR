using Application.CreatePCBA;
using BuildingBlocks.Application;
using BuildingBlocks.Integration;
using LINTest.Integration;
using LINTest.LinakDB;

namespace Application.CreateOrUpdateActuator;

public class ActuatorFound : IIntegrationEventListener<ActuatorFoundIntegrationEvent>
{
    private ICommandBus _bus;
    private readonly IPCBAService _pcbadao;

    public ActuatorFound(ICommandBus bus, IPCBAService pcbadao)
    {
        _bus = bus;
        this._pcbadao = pcbadao;
    }

    public Task Handle(ActuatorFoundIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var pcba = _pcbadao.GetPCBA(notification.PCBAUid);
        var pcbaCommand = CreatePCBACommand.Create(pcba.Uid.ToString(), pcba.ManufacturerNumber, pcba.ItemNumber, pcba.Software,
            pcba.ProductionDateCode);
        _bus.Send(pcbaCommand, cancellationToken);
        
        var actuatorCommand = CreateOrUpdateActuatorCommand.Create(notification.WorkOrderNumber, notification.SerailNumber,
            notification.PCBAUid);
        return _bus.Send(actuatorCommand, cancellationToken);
    }
}