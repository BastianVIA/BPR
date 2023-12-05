using BuildingBlocks.Application;
using TestResult.Domain.Entities;
using TestResult.Domain.Repositories;

namespace TestResult.Application.GetActuatorTestDetails;

public class GetActuatorTestQueryHandler : IQueryHandler<GetActuatorTestDetailsQuery, GetActuatorTestDetailsDto>
{
    private readonly ITestResultRepository _testResultRepository;

    public GetActuatorTestQueryHandler(ITestResultRepository testResultRepository)
    {
        _testResultRepository = testResultRepository;
    }

    public async Task<GetActuatorTestDetailsDto> Handle(GetActuatorTestDetailsQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var actuatorTests =
                await _testResultRepository.GetActuatorsTestDetails(request.WorkOrderNumber, request.SerialNumber,
                    request.Tester, request.Bay);

            return GetActuatorTestDetailsDto.From(actuatorTests);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}