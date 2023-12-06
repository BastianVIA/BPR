using Application.GetPCBAChangesForActuator;
using BuildingBlocks.Application;

namespace Application.GetActuatorsWithFilterAsCSV;

public class GetActuatorsWithFilterAsCSVQueryHandler : IQueryHandler<GetActuatorsWithFilterAsCSVQuery, byte[]>
{
    private readonly IQueryBus _bus;

    public GetActuatorsWithFilterAsCSVQueryHandler(IQueryBus bus)
    {
        _bus = bus;
    }

    public async Task<byte[]> Handle(GetActuatorsWithFilterAsCSVQuery request, CancellationToken cancellationToken)
    {
        var searchResult = await _bus.Send(request.FilterQuery, cancellationToken);
        var propsToIncludeAsStrings = request.PropertiesToInclude.ConvertAll(csvProperty => csvProperty.ToString().Replace("_", ""));
        var singleLines = GetActuatorsWithFilerAsCSVDto.From(searchResult);
        return CsvWriterHelper.WriteToCsv(singleLines.AllLines, propsToIncludeAsStrings);
    }
}