using BuildingBlocks.Application;

namespace Application.GetActuatorsWithFilter;

public class GetActuatorsWithFilterQuery : IQuery<GetActuatorsWithFilterDto>
{
    internal int? WorkOrderNumber { get; }
    internal int? SerialNumber { get; }
    internal string? PCBAUid { get; }
    internal string? ItemNumber { get; }
    internal int? ManufacturerNumber { get; }
    internal int? ProductionDateCode { get; }
    internal string? CommunicationProtocol { get; }
    internal string? ArticleNumber { get; }
    internal string? ArticleName { get; }
    internal DateTime? StartDate { get; }
    internal DateTime? EndDate { get; }

    private GetActuatorsWithFilterQuery(int? woNo, int? serialNo, string? pcbaUid, string? itemNo, int? manufacturerNo,
        int? productionDateCode, string? communicationProtocol, string? articleNumber, string? articleName,
        DateTime? startDate, DateTime? endDate)
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
        StartDate = startDate;
        EndDate = endDate;
    }

    public static GetActuatorsWithFilterQuery Create(int? woNo, int? serialNo, string? pcbaUid, string? itemNo,
        int? manufacturerNo, int? productionDateCode, string? communicationProtocol, string? articleNumber,
        string? articleName, DateTime? startDate, DateTime? endDate)
    {
        if (woNo == null && serialNo == null && productionDateCode == null && manufacturerNo == null &&
            productionDateCode == null && pcbaUid == null &&
            itemNo == null && communicationProtocol == null && articleNumber == null && articleName == null &&
            startDate == null && endDate == null)
        {
            throw new ArgumentException("Must specify at least one search parameter");
        }

        return new GetActuatorsWithFilterQuery(woNo, serialNo, pcbaUid, itemNo, manufacturerNo, productionDateCode,
            communicationProtocol, articleNumber, articleName, startDate, endDate);
    }
}