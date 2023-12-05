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
            var actuatorId = CompositeActuatorId.From(request.WorkOrderNumber, request.SerialNumber);
            var actuatortest = await _testResultRepository.GetActuatorTestDetails(actuatorId);
            return GetActuatorTestDetailsDto.From(actuatortest);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}