using Application.GetActuatorsWithFilter;
using BuildingBlocks.Application;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.Actuator;

[ApiController]
public class GetActuatorsWithFilterController : ControllerBase
{
    private readonly IQueryBus _bus;

    public GetActuatorsWithFilterController(IQueryBus bus)
    {
        _bus = bus;
    }

    [HttpGet()]
    [Route("api/[controller]")]
    [Tags("Actuator")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetActuatorWithFilterResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetAsync(
        [FromQuery] GetActuatorsWithFilterQuery query,
        CancellationToken cancellationToken)
    {
        query.Validate();
        var result = await _bus.Send(query, cancellationToken);
        return Ok(GetActuatorWithFilterResponse.From(result));
    }

    public class GetActuatorWithFilterResponse
    {
        public List<GetActuatorWithFilterActuator> Actuators { get; private set; }

        private GetActuatorWithFilterResponse()
        {
        }

        private GetActuatorWithFilterResponse(List<GetActuatorWithFilterActuator> actuator)
        {
            Actuators = actuator;
        }

        internal static GetActuatorWithFilterResponse From(GetActuatorsWithFilterDto result)
        {
            var actuators = result.ActuatorDtos.Select(actuator => GetActuatorWithFilterActuator.From(actuator)).ToList();
            return new GetActuatorWithFilterResponse(actuators);
        }
    }
}

public class GetActuatorWithFilterActuator
{
    public int WorkOrderNumber { get; private set; }
    public int SerialNumber { get; private set; }
    public GetActuatorWithFilterPCBA PCBA { get; private set; }
    public string CommunicationProtocol { get; private set; }
    public string ArticleNumber { get; private set; }
    public string ArticleName { get; private set; }
    public DateTime CreatedTime { get; private set; }

    internal static GetActuatorWithFilterActuator From(ActuatorDto result)
    {
        return new GetActuatorWithFilterActuator
        {
            WorkOrderNumber = result.WorkOrderNumber, SerialNumber = result.SerialNumber,
            PCBA = GetActuatorWithFilterPCBA.From(result.PCBA),
            CommunicationProtocol = result.CommunicationProtocol,
            ArticleNumber = result.ArticleNumber,
            ArticleName = result.ArticleName,
            CreatedTime = result.CreatedTime
        };
    }
}

public class GetActuatorWithFilterPCBA
{
    public string Uid { get; set; }
    public int ManufacturerNumber { get; private set; }
    public string ItemNumber { get; private set; }
    public string Software { get; private set; }
    public int ProductionDateCode { get; private set; }
    public string ConfigNo { get; private set; }


    private GetActuatorWithFilterPCBA(string pcbaUid, int manufacturerNumber, string itemNumber,
        int productionDateCode, string software, string configNo)
    {
        ManufacturerNumber = manufacturerNumber;
        Uid = pcbaUid;
        ItemNumber = itemNumber;
        ProductionDateCode = productionDateCode;
        Software = software;
        ConfigNo = configNo;
    }

    internal static GetActuatorWithFilterPCBA From(PCBADto result)
    {
        return new GetActuatorWithFilterPCBA(result.Uid, result.ManufacturerNumber, result.ItemNumber,
            result.ProductionDateCode, result.Software, result.ConfigNo);
    }
}