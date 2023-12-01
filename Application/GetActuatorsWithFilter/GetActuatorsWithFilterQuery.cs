using BuildingBlocks.Application;

namespace Application.GetActuatorsWithFilter;

public class GetActuatorsWithFilterQuery : IQuery<GetActuatorsWithFilterDto>
{
    internal int? ItemNumber { get; }
    internal int? ManufacturerNumber { get; }
    internal int? ProductionDateCode { get; }

    private GetActuatorsWithFilterQuery(int? itemNo, int? manufacturerNo, int? productionDateCode)
    {
        ItemNumber = itemNo;
        ManufacturerNumber = manufacturerNo;
        productionDateCode = productionDateCode;
    }

    public static GetActuatorsWithFilterQuery Create(int? itemNo, int? manufacturerNo, int? productionDateCode)
    {
        if (productionDateCode == null && manufacturerNo == null && productionDateCode == null)
        {
            throw new ArgumentException("(Filter cannot all be empty");
        }

        return new GetActuatorsWithFilterQuery(itemNo, manufacturerNo, productionDateCode);
    }
}