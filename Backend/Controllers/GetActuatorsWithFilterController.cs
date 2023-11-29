using Application.GetActuatorsWithFilter;
using BuildingBlocks.Application;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

public class GetActuatorsWithFilterController : ControllerBase
{
    private readonly IQueryBus _bus;

    public GetActuatorsWithFilterController(IQueryBus bus)
    {
        _bus = bus;
    }

    [HttpGet()]
    [Route("api/GetActuatorsWithFilter")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetActuatorWithFilterResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetAsync([FromQuery] int? itemNo, [FromQuery] int? manufacturerNo,
        [FromQuery] int? productionDateCode, CancellationToken cancellationToken)
    {
        var query = GetActuatorsWithFilterQuery.Create(itemNo, manufacturerNo, productionDateCode);
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
            List<GetActuatorWithFilterActuator> actuators = new List<GetActuatorWithFilterActuator>();
            foreach (var actuator in result.ActuatorDtos)
            {
                actuators.Add(GetActuatorWithFilterActuator.From(actuator));
            }

            return new GetActuatorWithFilterResponse(actuators);
        }
    }


    public class GetActuatorWithFilterPCBA
    {
        public int ItemNumber { get; }
        public int ManufacturerNumber { get; }
        public int ProductionDateCode { get; }
        public string PCBAUid { get; }


        public GetActuatorWithFilterPCBA(int manufacturerNumber, string pcbaUid)
        {
            ManufacturerNumber = manufacturerNumber;
            PCBAUid = pcbaUid;
        }

        internal static GetActuatorWithFilterPCBA From(PCBADto result)
        {
            return new GetActuatorWithFilterPCBA(result.ManufacturerNumber, result.Uid);
        }

        // TODO
        // public GetActuatorWithFilterPCBA(int itemNo, int manufacturerNo, int productionDateCode, int pcbaUid)
        // {
        //     this.itemNo = itemNo;
        //     this.manufacturerNo = manufacturerNo;
        //     this.productionDateCode = productionDateCode;
        //        this.pcbaUid = pcbauid;
        // }


        //
        // internal static GetActuatorWithFilterPCBA From(PCBA result)
        // {
        //     return new GetActuatorWithFilterPCBA(result.itemNo, result.ManufacturerNumber, result.productionDateCode);
        // }
    }

    public class GetActuatorWithFilterActuator
    {
        public int WorkOrderNumber { get; private set; }
        public int SerialNumber { get; private set; }

        public GetActuatorWithFilterPCBA Pcba { get; private set; }


        internal static GetActuatorWithFilterActuator From(ActuatorDTO result)
        {
            return new GetActuatorWithFilterActuator
            {
                WorkOrderNumber = result.WorkOrderNumber, SerialNumber = result.SerialNumber,
                Pcba = GetActuatorWithFilterPCBA.From(result.Pcba)
            };
        }
    }
}