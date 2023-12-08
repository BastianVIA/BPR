using BuildingBlocks.Application;
using Domain.Entities;
using Domain.Repositories;
using TestResult.Domain.Repositories;

namespace Application.GetStartUpAmounts;

public class GetStartUpAmountsQueryHandler : IQueryHandler<GetStartUpAmountsQuery, GetStartUpAmountsDto>
{
    private readonly IActuatorRepository _actuatorRepository;
    private readonly ITestResultRepository _testResultRepository;
    private readonly ITestErrorRepository _testErrorRepository;

    public GetStartUpAmountsQueryHandler(IActuatorRepository actuatorRepository, ITestResultRepository testResultRepository, ITestErrorRepository testErrorRepository)
    {
        _actuatorRepository = actuatorRepository;
        _testResultRepository = testResultRepository;
        _testErrorRepository = testErrorRepository;
    }

    public async Task<GetStartUpAmountsDto> Handle(GetStartUpAmountsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var actuatorAmount = await _actuatorRepository.GetTotalActuatorAmount();
            var testResultAmount = await _testResultRepository.GetTotalTestResultAmount();
            var testErrorAmount = await _testErrorRepository.GetTotalTestErrorAmounts();
            var testResultWithErrorAmount = await _testResultRepository.GetTotalTestResultWithErrorsAmount();
            var testResultWithoutErrorAmount = await _testResultRepository.GetTotalTestResultWithoutErrorsAmount();
            var startUpAmounts = new StartUpAmounts(actuatorAmount, testResultAmount, testErrorAmount,
                testResultWithErrorAmount, testResultWithoutErrorAmount);
            return GetStartUpAmountsDto.From(startUpAmounts);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}