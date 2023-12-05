using Application.GetActuatorsWithFilter;

namespace Application.GetActuatorsWithFilterAsCSV;

public class GetActuatorsWithFilerAsCSVDto
{
    public List<GetActuatorsWithFilerAsCSVSingleLine> AllLines { get; }

    private GetActuatorsWithFilerAsCSVDto()
    {
    }

    private GetActuatorsWithFilerAsCSVDto(List<GetActuatorsWithFilerAsCSVSingleLine> allLines)
    {
        AllLines = allLines;
    }

    public static GetActuatorsWithFilerAsCSVDto From(GetActuatorsWithFilterDto dto)
    {
        List<GetActuatorsWithFilerAsCSVSingleLine> allLines = new();
        foreach (var actuatorDto in dto.ActuatorDtos)
        {
            allLines.Add(GetActuatorsWithFilerAsCSVSingleLine.From(actuatorDto));
        }

        return new GetActuatorsWithFilerAsCSVDto(allLines);
    }
}

public class GetActuatorsWithFilerAsCSVSingleLine
{
    public int WorkOrderNumber { get; private set; }
    public int SerialNumber { get; private set; }
    public string CommunicationProtocol { get; private set; }
    public string ArticleNumber { get; private set; }
    public string ArticleName { get; private set; }
    public DateTime CreatedTime { get; private set; }
    public string PCBAUid { get; set; }
    public int PCBAManufacturerNumber { get; private set; }
    public string PCBAItemNumber { get; private set; }
    public string PCBASoftware { get; private set; }
    public int PCBAProductionDateCode { get; private set; }
    public string PCBAConfigNo { get; private set; }


    private GetActuatorsWithFilerAsCSVSingleLine()
    {
    }

    public static GetActuatorsWithFilerAsCSVSingleLine From(ActuatorDTO actuatorDto)
    {
        return new GetActuatorsWithFilerAsCSVSingleLine
        {
            WorkOrderNumber = actuatorDto.WorkOrderNumber,
            SerialNumber = actuatorDto.SerialNumber,
            CommunicationProtocol = actuatorDto.CommunicationProtocol,
            ArticleNumber = actuatorDto.ArticleNumber,
            ArticleName = actuatorDto.ArticleName,
            CreatedTime = actuatorDto.CreatedTime,
            PCBAUid = actuatorDto.PCBA.Uid,
            PCBAManufacturerNumber = actuatorDto.PCBA.ManufacturerNumber,
            PCBAItemNumber = actuatorDto.PCBA.ItemNumber,
            PCBASoftware = actuatorDto.PCBA.Software,
            PCBAProductionDateCode = actuatorDto.PCBA.ProductionDateCode,
            PCBAConfigNo = actuatorDto.PCBA.ConfigNo
        };
    }
}