using BuildingBlocks.Application;

namespace Application.GetActuatorsWithFilter;

public class GetActuatorsWithFilterQuery : IQuery<GetActuatorsWithFilterDto>
{
    public int? WorkOrderNumber { get; set; }
    public int? SerialNumber { get; set; }
    public string? PCBAUid { get; set; }
    public string? ItemNumber { get; set; }
    public int? ManufacturerNumber { get; set; }
    public int? ProductionDateCode { get; set; }
    public string? CommunicationProtocol { get; set; }
    public string? ArticleNumber { get; set; }
    public string? ArticleName { get; set; }
    public string? ConfigNo { get; set; }
    public string? Software { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public GetActuatorsWithFilterQuery(){}
    private GetActuatorsWithFilterQuery(int? woNo, int? serialNo, string? pcbaUid, string? itemNo, int? manufacturerNo,
        int? productionDateCode, string? communicationProtocol, string? articleNumber, string? articleName,
       string? configNo, string? software, DateTime? startDate, DateTime? endDate)
    {
        WorkOrderNumber = woNo;
        SerialNumber = serialNo;
        PCBAUid = pcbaUid;
        ItemNumber = itemNo;
        ManufacturerNumber = manufacturerNo;
        ProductionDateCode = productionDateCode;
        CommunicationProtocol = communicationProtocol;
        ArticleNumber = articleNumber;
        ArticleName = articleName;
        ConfigNo = configNo;
        Software = software;
        StartDate = startDate;
        EndDate = endDate;
    }

    public void Validate()
    {
        if (IsNotValid())
        {
            throw new ArgumentException("Must specify at least one search parameter");
        }
    }

    private bool IsNotValid()
    {
        return WorkOrderNumber is null && SerialNumber is null && ProductionDateCode is null && ManufacturerNumber is null &&
               ProductionDateCode is null && PCBAUid is null && ItemNumber is null && CommunicationProtocol is null && 
               ArticleNumber is null && ArticleName is null && ConfigNo is null && Software is null &&
               StartDate is null && EndDate is null;
    }
}