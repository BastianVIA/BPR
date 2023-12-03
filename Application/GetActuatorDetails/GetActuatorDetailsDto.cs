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
        PCBADto pcbaDto = PCBADto.From(actuator.PCBA.Uid, actuator.PCBA.ManufacturerNumber, actuator.PCBA.ItemNumber, actuator.PCBA.Software, actuator.PCBA.ProductionDateCode);
        return new GetActuatorDetailsDto(pcbaDto);
    }
}

public class PCBADto
{
    public string Uid { get; set; }
    public int ManufacturerNumber { get; set; }
    public string ItemNumber { get;  set; }
    public string Software { get;  set; }
    public int ProductionDateCode { get;  set; }

    internal static PCBADto From(string pcbaUid, int manufacturerNo, string itemNumber, string software, int productionDateCode)
    {
        return new PCBADto
        {
            Uid = pcbaUid,
            ManufacturerNumber = manufacturerNo,
            ItemNumber = itemNumber,
            Software = software,
            ProductionDateCode = productionDateCode
        };
    }
}