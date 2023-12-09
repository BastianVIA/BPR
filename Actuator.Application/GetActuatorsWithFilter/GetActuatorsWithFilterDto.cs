using Domain.Entities;

namespace Application.GetActuatorsWithFilter;

public class GetActuatorsWithFilterDto
{
    public List<ActuatorDto> ActuatorDtos { get; }

    private GetActuatorsWithFilterDto() { }

    private GetActuatorsWithFilterDto(List<ActuatorDto> actuatorDtos)
    {
        ActuatorDtos = actuatorDtos;
    }

    internal static GetActuatorsWithFilterDto From(List<Actuator> actuators)
    {
        List<ActuatorDto> actuatorDtos = new List<ActuatorDto>();

        foreach (var actuator in actuators)
        {
            actuatorDtos.Add(ActuatorDto.From(actuator));
        }

        return new GetActuatorsWithFilterDto(actuatorDtos);
    }
}

public class ActuatorDto
{
    public int WorkOrderNumber { get; private set; }
    public int SerialNumber { get; private set; }
    public PCBADto PCBA { get; private set; }
    
    public string CommunicationProtocol { get; private set; }
    public string ArticleNumber { get; private set; }
    public string ArticleName { get; private set; }
    public DateTime CreatedTime { get; private set; }

    internal static ActuatorDto From(Actuator actuator)
    {
        return new ActuatorDto
        {
            WorkOrderNumber = actuator.Id.WorkOrderNumber,
            SerialNumber = actuator.Id.SerialNumber,
            PCBA = PCBADto.From(actuator.PCBA),
            CommunicationProtocol = actuator.CommunicationProtocol,
            ArticleNumber = actuator.ArticleNumber,
            ArticleName = actuator.ArticleName,
            CreatedTime = actuator.CreatedTime
        };
    }
}

public class PCBADto
{
    public string Uid { get; set; }
    public int ManufacturerNumber { get; private set; }
    public string ItemNumber { get; private set; }
    public string Software { get; private set; }
    public int ProductionDateCode { get; private set; }
    public string ConfigNo { get; private set; }

    internal static PCBADto From(PCBA pcba)
    {
        return new PCBADto
        {
            Uid = pcba.Uid,
            ManufacturerNumber = pcba.ManufacturerNumber,
            ProductionDateCode = pcba.ProductionDateCode,
            ItemNumber = pcba.ItemNumber,
            Software = pcba.Software,
            ConfigNo = pcba.ConfigNo
        };
    }
}
