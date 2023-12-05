using Application.GetActuatorDetails;
using BuildingBlocks.Application;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
public class GetActuatorDetailsController : ControllerBase
{
    private readonly IQueryBus _bus;

    public GetActuatorDetailsController(IQueryBus bus)
    {
        _bus = bus;
    }

    [HttpGet()]
    [Route("api/GetActuatorDetails/{woNo}/{serialNo}")]
    [Tags("Actuator")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetActuatorDetailsResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetAsync(int woNo, int serialNo, CancellationToken cancellationToken)
    {
        var query = GetActuatorDetailsQuery.Create(woNo, serialNo);
        var result = await _bus.Send(query, cancellationToken);
        return Ok(GetActuatorDetailsResponse.From(result));
    }
}

public class GetActuatorDetailsResponse
{
    public int WorkOrderNumber { get; private set; }
    public int SerialNumber { get; private set; }
    public GetActuatorDetailsPCBA PCBA { get; }
    public string CommunicationProtocol { get; private set; }
    public string ArticleNumber { get; private set; }
    public string ArticleName { get; private set; }
    public DateTime CreatedTime { get; private set; }

    private GetActuatorDetailsResponse()
    {
    }

    private GetActuatorDetailsResponse(int woNo, int serialNumber, string communicationProtocol, string articleNumber,
        string articleName, DateTime createdTime, GetActuatorDetailsPCBA pcba)

    {
        WorkOrderNumber = woNo;
        SerialNumber = serialNumber;
        PCBA = pcba;
        CommunicationProtocol = communicationProtocol;
        ArticleNumber = articleNumber;
        ArticleName = articleName;
        CreatedTime = createdTime;
    }

    internal static GetActuatorDetailsResponse From(GetActuatorDetailsDto result)
    {
        var pcba = GetActuatorDetailsPCBA.From(result.PCBADto.Uid, result.PCBADto.ManufacturerNumber,
            result.PCBADto.ItemNumber,
            result.PCBADto.Software, result.PCBADto.ProductionDateCode, result.PCBADto.ConfigNo);

        return new GetActuatorDetailsResponse(result.WorkOrderNumber, result.SerialNumber, result.CommunicationProtocol,
            result.ArticleNumber, result.ArticleName, result.CreatedTime, pcba);
    }
}

public class GetActuatorDetailsPCBA
{
    public string Uid { get; }
    public int ManufacturerNumber { get; }
    public string ItemNumber { get; }
    public string Software { get; }
    public int ProductionDateCode { get; }
    public string ConfigNo { get; }

    private GetActuatorDetailsPCBA()
    {
    }

    private GetActuatorDetailsPCBA(string pcbaUid, int manufacturerNo, string itemNumber, string software,
        int productionDateCode, string configNo)
    {
        Uid = pcbaUid;
        ManufacturerNumber = manufacturerNo;
        ItemNumber = itemNumber;
        Software = software;
        ProductionDateCode = productionDateCode;
        ConfigNo = configNo;
    }

    public static GetActuatorDetailsPCBA From(string pcbaUid, int manufacturerNo, string itemNumber, string software,
        int productionDateCode, string configNo)
    {
        return new GetActuatorDetailsPCBA(pcbaUid, manufacturerNo, itemNumber, software, productionDateCode, configNo);
    }
}