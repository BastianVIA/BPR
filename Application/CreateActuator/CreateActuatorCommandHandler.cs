using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using BuildingBlocks.Application;

namespace Application.CreateActuator;

public class CreateActuatorCommandHandler:ICommandHandler<CreateActuatorCommand>
{
    public async Task Handle(CreateActuatorCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine("serial : " + request.SerialNumber + " WorkOrder no :" + request.WorkOrderNumber + " PCBA Id: " + request.PCBAUid);
    }
}