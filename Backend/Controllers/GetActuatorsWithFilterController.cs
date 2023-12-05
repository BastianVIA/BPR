﻿using Application.GetActuatorsWithFilter;
using BuildingBlocks.Application;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
public class GetActuatorsWithFilterController : ControllerBase
{
    private readonly IQueryBus _bus;

    public GetActuatorsWithFilterController(IQueryBus bus)
    {
        _bus = bus;
    }

    [HttpGet()]
    [Route("api/GetActuatorsWithFilter")]
    [Tags("Actuator")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetActuatorWithFilterResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetAsync(
        [FromQuery] int? woNo,
        [FromQuery] int? serialNo, 
        [FromQuery] string? pcbaUid,
        [FromQuery] string? itemNo,
        [FromQuery] int? manufacturerNo,
        [FromQuery] int? productionDateCode,
        [FromQuery] string? communicationProtocol,
        [FromQuery] string? articleNumber,
        [FromQuery] string? articleName,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate,
        CancellationToken cancellationToken)
    {
        var query = GetActuatorsWithFilterQuery.Create(woNo, serialNo, pcbaUid, itemNo, manufacturerNo, productionDateCode, communicationProtocol, articleNumber, articleName, startDate, endDate);
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

    public class GetActuatorWithFilterActuator
    {
        public int WorkOrderNumber { get; private set; }
        public int SerialNumber { get; private set; }
        public GetActuatorWithFilterPCBA PCBA { get; private set; }
        public string CommunicationProtocol { get; private set; }
        public string ArticleNumber { get; private set; }
        public string ArticleName { get; private set; }
        public DateTime CreatedTime { get; private set; }

        internal static GetActuatorWithFilterActuator From(ActuatorDTO result)
        {
            return new GetActuatorWithFilterActuator
            {
                WorkOrderNumber = result.WorkOrderNumber, SerialNumber = result.SerialNumber,
                PCBA = GetActuatorWithFilterPCBA.From(result.Pcba),
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
}