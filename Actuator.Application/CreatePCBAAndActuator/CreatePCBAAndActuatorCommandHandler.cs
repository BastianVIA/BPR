using Application.CreateOrUpdateActuator;
using Application.CreatePCBA;
using BuildingBlocks.Application;
using LINTest.LinakDB;

namespace Application.CreatePCBAAndActuator;

public class CreatePCBAAndActuatorCommandHandler : ICommandHandler<CreatePCBAAndActuatorCommand>
{
    private ICommandBus _bus;
    private readonly IPCBAService _pcbadao;

    public CreatePCBAAndActuatorCommandHandler(ICommandBus bus, IPCBAService pcbadao)
    {
        _bus = bus;
        _pcbadao = pcbadao;
    }

    public Task Handle(CreatePCBAAndActuatorCommand request, CancellationToken cancellationToken)
    {
        var pcba = _pcbadao.GetPCBA(request.PCBAUid);
        var pcbaCommand = CreatePCBACommand.Create(pcba.Uid.ToString(), pcba.ManufacturerNumber, pcba.ItemNumber, pcba.Software,
            pcba.ProductionDateCode);
        _bus.Send(pcbaCommand, cancellationToken);
        
        var actuatorCommand = CreateOrUpdateActuatorCommand.Create(request.WorkOrderNumber, request.SerialNumber,
            request.PCBAUid, request.ArticleNumber, request.ArticleNumber,
            request.CommunicationProtocol, request.CreatedTime);
        return _bus.Send(actuatorCommand, cancellationToken);    }
}