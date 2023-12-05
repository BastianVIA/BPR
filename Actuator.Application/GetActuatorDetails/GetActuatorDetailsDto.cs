using Domain.Entities;

namespace Application.GetActuatorDetails;

public class GetActuatorDetailsDto
{
    public int WorkOrderNumber { get; }
    public int SerialNumber { get; }
    public PCBADto PCBADto { get; }
    public string CommunicationProtocol { get; }
    public string ArticleNumber { get; }
    public string ArticleName { get; }
    public DateTime CreatedTime { get; }
    
    private GetActuatorDetailsDto(){}
    private GetActuatorDetailsDto(int woNo, int serialNumber, PCBADto pcbaDto, string communicationProtocol, string articleNumber, string articleName, DateTime createdTime)
    {
        PCBADto = pcbaDto;
        WorkOrderNumber = woNo;
        SerialNumber = serialNumber;
        CommunicationProtocol = communicationProtocol;
        ArticleNumber = articleNumber;
        ArticleName = articleName;
        CreatedTime = createdTime;
    }

    internal static GetActuatorDetailsDto From(Actuator actuator)
    {
        PCBADto pcbaDto = PCBADto.From( actuator.PCBA.Uid, actuator.PCBA.ManufacturerNumber, actuator.PCBA.ItemNumber, actuator.PCBA.Software, actuator.PCBA.ProductionDateCode, actuator.PCBA.ConfigNo);
        return new GetActuatorDetailsDto(actuator.Id.WorkOrderNumber, actuator.Id.SerialNumber, pcbaDto, actuator.CommunicationProtocol,actuator.ArticleNumber,actuator.ArticleName, actuator.CreatedTime);
    }
}

public class PCBADto
{
    public string Uid { get; set; }
    public int ManufacturerNumber { get; set; }
    public string ItemNumber { get; set; }
    public string Software { get; set; }
    public int ProductionDateCode { get; set; }
    public string ConfigNo { get; set; }

    internal static PCBADto From(string pcbaUid, int manufacturerNo, string itemNumber, string software,
        int productionDateCode, string configNo)
    {
        return new PCBADto
        {
            Uid = pcbaUid,
            ManufacturerNumber = manufacturerNo,
            ItemNumber = itemNumber,
            Software = software,
            ProductionDateCode = productionDateCode,
            ConfigNo = configNo
        };
    }
}