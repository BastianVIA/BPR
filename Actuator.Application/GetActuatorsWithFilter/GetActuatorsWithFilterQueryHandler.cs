using BuildingBlocks.Application;
using Domain.Entities;
using Domain.Repositories;

namespace Application.GetActuatorsWithFilter;

public class GetActuatorsWithFilterQueryHandler : IQueryHandler<GetActuatorsWithFilterQuery, GetActuatorsWithFilterDto>
{
    private readonly IActuatorRepository _actuatorRepository;

    public GetActuatorsWithFilterQueryHandler(IActuatorRepository actuatorRepository)
    {
        _actuatorRepository = actuatorRepository;
    }

    public async Task<GetActuatorsWithFilterDto> Handle(GetActuatorsWithFilterQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var actuators = await _actuatorRepository.GetActuatorsWithFilter(request.WorkOrderNumber, request.SerialNumber, request.PCBAUid, request.ItemNumber,
                request.ManufacturerNumber, request.ProductionDateCode, request.CommunicationProtocol, request.ArticleNumber, request.ArticleName, request.ConfigNo, request.Software, request.StartDate, request.EndDate);
            return GetActuatorsWithFilterDto.From(actuators);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}