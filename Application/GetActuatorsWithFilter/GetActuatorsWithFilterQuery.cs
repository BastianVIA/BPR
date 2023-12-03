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

    private GetActuatorsWithFilterQuery(int? woNo, int? serialNo, string? pcbaUid, string? itemNo, int? manufacturerNo, int? productionDateCode)
    {
        WorkOrderNumber = woNo;
        SerialNumber = serialNo;
        PCBAUid = pcbaUid;
        ItemNumber = itemNo;
        ManufacturerNumber = manufacturerNo;
        ProductionDateCode = productionDateCode;
    }

    public static GetActuatorsWithFilterQuery Create(int? woNo, int? serialNo, string? pcbaUid, string? itemNo, int? manufacturerNo, int? productionDateCode)
    {
        if (woNo == null && serialNo == null && productionDateCode == null && manufacturerNo == null &&
            productionDateCode == null && pcbaUid == null &&
            itemNo == null)
        {
            throw new ArgumentException("Must specify at least one search parameter");
        }

        return new GetActuatorsWithFilterQuery(woNo, serialNo, pcbaUid, itemNo, manufacturerNo, productionDateCode);
    }
}