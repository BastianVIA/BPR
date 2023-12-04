using Application.GetActuatorDetails;
using Domain.Entities;

namespace Application.GetActuatorsWithFilter;

public class GetActuatorsWithFilterDto
{
    public List<ActuatorDTO> ActuatorDtos { get; }

    private GetActuatorsWithFilterDto()
    {
    }

    private GetActuatorsWithFilterDto(List<ActuatorDTO> actuatorDtos)
    {
        ActuatorDtos = actuatorDtos;
    }

    internal static GetActuatorsWithFilterDto From(List<Actuator> actuators)
    {
        List<ActuatorDTO> actuatorDtos = new List<ActuatorDTO>();

        foreach (var actuator in actuators)
        {
            actuatorDtos.Add(ActuatorDTO.From(actuator));
        }

        return new GetActuatorsWithFilterDto(actuatorDtos);
    }
}

public class PCBADto
{
    public string Uid { get; set; }
    public int ManufacturerNumber { get; set; }
    public int ProductionDateCode { get; set; }
    public string ItemNumber { get; set; }

    internal static PCBADto From(PCBA pcba)
    {
        return new PCBADto
        {
            Uid = pcba.Uid,
            ManufacturerNumber = pcba.ManufacturerNumber,
            ProductionDateCode = pcba.ProductionDateCode,
            ItemNumber = pcba.ItemNumber
        };
    }
}

public class ActuatorDTO
{
    public int WorkOrderNumber { get; private set; }
    public int SerialNumber { get; private set; }
    public PCBADto Pcba { get; private set; }

    internal static ActuatorDTO From(Actuator actuator)
    {
        return new ActuatorDTO
        {
            WorkOrderNumber = actuator.Id.WorkOrderNumber,
            SerialNumber = actuator.Id.SerialNumber,
            Pcba = PCBADto.From(actuator.PCBA)
        };
    }
}