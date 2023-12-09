using Application.CreateOrUpdateActuator;
using Application.CreatePCBA;
using BuildingBlocks.Application;
using LINTest.LinakDB;

namespace Application.CreatePCBAAndActuator;

public class CreatePCBAAndActuatorCommandHandler : ICommandHandler<CreatePCBAAndActuatorCommand>
{
    private ICommandBus _bus;
    private readonly IPCBAService _pcbaService;

    public CreatePCBAAndActuatorCommandHandler(ICommandBus bus, IPCBAService pcbaService)
    {
        _bus = bus;
        _pcbaService = pcbaService;
    }

    public Task Handle(CreatePCBAAndActuatorCommand request, CancellationToken cancellationToken)
    {
        var pcba = _pcbaService.GetPCBA(request.PCBAUid);
        var pcbaCommand = CreateOrUpdatePCBACommand.Create(pcba.Uid.ToString(), pcba.ManufacturerNumber, pcba.ItemNumber, pcba.Software,
            pcba.ProductionDateCode, pcba.ConfigNo);
        _bus.Send(pcbaCommand, cancellationToken);
        
        var actuatorCommand = CreateOrUpdateActuatorCommand.Create(request.WorkOrderNumber, request.SerialNumber,
            request.PCBAUid, request.ArticleNumber, request.ArticleName,
            request.CommunicationProtocol, request.CreatedTime);
        return _bus.Send(actuatorCommand, cancellationToken);    }
}