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
    internal string? ConfigNo { get; }
    internal string? Software { get; }
    internal DateTime? StartDate { get; }
    internal DateTime? EndDate { get; }

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

    public static GetActuatorsWithFilterQuery Create(int? woNo, int? serialNo, string? pcbaUid, string? itemNo,
        int? manufacturerNo, int? productionDateCode, string? communicationProtocol, string? articleNumber,
        string? articleName, string? configNo, string? software, DateTime? startDate, DateTime? endDate)
    {
        if (ParametersAreNull(woNo,serialNo,pcbaUid,itemNo, manufacturerNo, productionDateCode, communicationProtocol, articleNumber, articleName, configNo, software,
            startDate , endDate))
        {
            throw new ArgumentException("Must specify at least one search parameter");
        }

        return new GetActuatorsWithFilterQuery(woNo, serialNo, pcbaUid, itemNo, manufacturerNo, productionDateCode,
            communicationProtocol, articleNumber, articleName, configNo, software, startDate, endDate);
    }

    private static bool ParametersAreNull(int? woNo, int? serialNo, string? pcbaUid, string? itemNo, int? manufacturerNo, int? productionDateCode,string? communicationProtocol, string? articleNumber, string? articleName, string? configNo, string? software, DateTime? startDate, DateTime? endDate)
    {
        return woNo is null && serialNo is null && productionDateCode is null && manufacturerNo is null &&
            productionDateCode is null && pcbaUid is null && itemNo is null && communicationProtocol is null && 
            articleNumber is null && articleName is null && configNo is null && software is null &&
            startDate is null && endDate is null;
    }
}