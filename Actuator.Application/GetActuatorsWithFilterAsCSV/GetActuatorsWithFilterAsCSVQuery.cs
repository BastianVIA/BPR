using Application.GetActuatorsWithFilter;
using BuildingBlocks.Application;

namespace Application.GetActuatorsWithFilterAsCSV;

public class GetActuatorsWithFilterAsCSVQuery : IQuery<byte[]>
{
    public GetActuatorsWithFilterQuery FilterQuery { get; }
    public List<CsvProperties> PropertiesToInclude { get; }

    private GetActuatorsWithFilterAsCSVQuery()
    {
    }

    private GetActuatorsWithFilterAsCSVQuery(GetActuatorsWithFilterQuery filterQuery,
        List<CsvProperties> propertiesToInclude)
    {
        FilterQuery = filterQuery;
        PropertiesToInclude = propertiesToInclude;
    }

    public static GetActuatorsWithFilterAsCSVQuery Create(GetActuatorsWithFilterQuery filterQuery,
        List<CsvProperties> propertiesToInclude)
    {
        return new GetActuatorsWithFilterAsCSVQuery(filterQuery, propertiesToInclude);
    }
}

