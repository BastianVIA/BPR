using Application.GetStartUpAmounts;
using BuildingBlocks.Application;
using Domain.Entities;
using Domain.RepositoryInterfaces;

namespace Application.GetActuatorStartUpAmounts;

public class GetActuatorStartUpAmountsQueryHandler : IQueryHandler<GetActuatorStartUpAmountsQuery, GetActuatorStartUpAmountsDto>
{
    private readonly IActuatorRepository _actuatorRepository;

    public GetActuatorStartUpAmountsQueryHandler(IActuatorRepository actuatorRepository)
    {
        _actuatorRepository = actuatorRepository;
    }

    public async Task<GetActuatorStartUpAmountsDto> Handle(GetActuatorStartUpAmountsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var actuatorAmount = await _actuatorRepository.GetTotalActuatorAmount();
            var startUpAmounts = new ActuatorStartUpAmounts(actuatorAmount);
            return GetActuatorStartUpAmountsDto.From(startUpAmounts);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}