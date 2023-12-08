using BuildingBlocks.Application;
using TestResult.Domain.Repositories;

namespace TestResult.Application.GetTestResultsWithFilter;

public class GetTestResultsWithFilterQueryHandler : IQueryHandler<GetTestResultsWithFilterQuery, GetTestResultsWithFilterDto>
{
    private readonly ITestResultRepository _testResultRepository;

    public GetTestResultsWithFilterQueryHandler(ITestResultRepository testResultRepository)
    {
        _testResultRepository = testResultRepository;
    }

    public async Task<GetTestResultsWithFilterDto> Handle(GetTestResultsWithFilterQuery request,
        CancellationToken cancellationToken)
    {
            var actuatorTests =
                await _testResultRepository.GetActuatorsTestDetails(request.WorkOrderNumber, request.SerialNumber,
                    request.Tester, request.Bay, request.StartDate, request.EndDate);
        
            return GetTestResultsWithFilterDto.From(actuatorTests);
    }
}