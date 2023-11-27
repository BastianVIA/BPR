using Domain.Entities;

namespace Application.GetActuatorFromPCBA;

public class GetActuatorFromPCBADto
{
    public List<GetActuatorFromPCBAActuatordto> Actuators = new();

    private GetActuatorFromPCBADto()
    {
    }

    private GetActuatorFromPCBADto(List<GetActuatorFromPCBAActuatordto> actuators)
    {
        Actuators = actuators;
    }

    internal static GetActuatorFromPCBADto From(List<Actuator> actuators)
    {
        List<GetActuatorFromPCBAActuatordto> dtos = new();
        foreach (var actuator in actuators)
        {
            dtos.Add(GetActuatorFromPCBAActuatordto.From(
                actuator.Id.WorkOrderNumber, 
                actuator.Id.SerialNumber,
                actuator.PCBA.Uid,
                actuator.PCBA.ManufacturerNumber
                ));
        }

        return new GetActuatorFromPCBADto(dtos);
    }
}

public class GetActuatorFromPCBAActuatordto
{
    public int WorkOrderNumber { get; }
    public int SerialNumber { get; }
    public string Uid { get; }
    public int ManufacturerNumber { get; }

    private GetActuatorFromPCBAActuatordto() { }

    private GetActuatorFromPCBAActuatordto(int woNo, int serialNo, string uid, int manufacturerNo)
    {
        Uid = uid;
        WorkOrderNumber = woNo;
        SerialNumber = serialNo;
        ManufacturerNumber = manufacturerNo;
    }

    internal static GetActuatorFromPCBAActuatordto From(int woNo, int serialNo, string uid, int manufacturerNo)
    {
        return new GetActuatorFromPCBAActuatordto(woNo, serialNo, uid, manufacturerNo);
    }

}