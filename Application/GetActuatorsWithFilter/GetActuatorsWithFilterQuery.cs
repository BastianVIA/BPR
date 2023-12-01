using BuildingBlocks.Application;

namespace Application.GetActuatorsWithFilter;

public class GetActuatorsWithFilterQuery: IQuery<GetActuatorsWithFilterDto>
{    
    internal string? PCBAUid { get; }
    internal string? ItemNumber { get; }
    internal int? ManufacturerNumber { get; }
    internal int? ProductionDateCode { get; }
    private GetActuatorsWithFilterQuery(string? pcbaUid, string? itemNo, int? manufacturerNo, int? productionDateCode)
    {
        PCBAUid = pcbaUid;
        ItemNumber = itemNo;
        ManufacturerNumber = manufacturerNo;
        ProductionDateCode = productionDateCode;
    }

    
    public static GetActuatorsWithFilterQuery Create(string? pcbaUid, string? itemNo, int? manufacturerNo, int? productionDateCode)
    {
        if (productionDateCode == null && manufacturerNo == null && productionDateCode == null && pcbaUid ==null && itemNo ==null )
        {
            throw new ArgumentException("(Filter cannot all be empty");
        }

        return new GetActuatorsWithFilterQuery(pcbaUid, itemNo, manufacturerNo, productionDateCode);

    }
}