using Domain.Entities;

namespace Application.GetActuatorDetails;

public class GetActuatorDetailsDto
{
    public PCBADto PCBADto { get; }
    
    private GetActuatorDetailsDto(){}
    private GetActuatorDetailsDto(PCBADto pcbaDto)
    {
        PCBADto = pcbaDto;
    }
    internal static GetActuatorDetailsDto From(Actuator actuator)
    {
        PCBADto pcbaDto = PCBADto.From(actuator.PCBA.Uid, actuator.PCBA.ManufacturerNumber);
        return new GetActuatorDetailsDto(pcbaDto);
    }
}

public class PCBADto
{
    public string Uid { get; set; }
    public int ManufacturerNumber { get; set; }

    internal static PCBADto From(string pcbaUid, int manufacturerNo)
    {
        return new PCBADto
        {
            Uid = pcbaUid,
            ManufacturerNumber = manufacturerNo
        };
    }
}