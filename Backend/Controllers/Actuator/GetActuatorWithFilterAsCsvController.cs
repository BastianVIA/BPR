using Application.GetActuatorsWithFilter;
using Application.GetActuatorsWithFilterAsCSV;
using BuildingBlocks.Application;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.Actuator;

[ApiController]
public class GetActuatorWithFilterAsCsvController : ControllerBase
{
    private readonly IQueryBus _bus;

    public GetActuatorWithFilterAsCsvController(IQueryBus bus)
    {
        _bus = bus;
    }

    [HttpGet]
    [Route("api/[controller]")]
    [Tags("Actuator")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<FileResult> GetAsync(
        [FromQuery] GetActuatorsWithFilterQuery filterQuery,
        [FromQuery] List<CsvProperties> propertiesToIncludeInFile,
        CancellationToken cancellationToken)
    {
        filterQuery.Validate();
        var query = GetActuatorsWithFilterAsCSVQuery.Create(filterQuery, propertiesToIncludeInFile);
        var result = await _bus.Send(query, cancellationToken);
        return File(result, "text/csv", "actuators.csv");
    }
}