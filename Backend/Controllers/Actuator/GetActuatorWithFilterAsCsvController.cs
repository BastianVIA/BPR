using Application.GetActuatorsWithFilter;
using Application.GetActuatorsWithFilterAsCSV;
using BuildingBlocks.Application;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
public class GetActuatorWithFilterAsCsvController : ControllerBase
{
    private readonly IQueryBus _bus;

    public GetActuatorWithFilterAsCsvController(IQueryBus bus)
    {
        _bus = bus;
    }

    [HttpGet]
    [Route("api/GetActuatorsWithFilterAsCsv")]
    [Tags("Actuator")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetAsync(
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