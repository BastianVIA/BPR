using BuildingBlocks.Application;
using TestResult.Domain.RepositoryInterfaces;

namespace TestResult.Application.GetNumberOfTestResultsForTimeInterval;

public class GetNumberOfTestResultsForTimeIntervalQueryHandler : IQueryHandler<GetNumberOfTestResultsForTimeIntervalQuery, int>
{
    private readonly ITestResultRepository _resultRepository;

    public GetNumberOfTestResultsForTimeIntervalQueryHandler(ITestResultRepository resultRepository)
    {
        _resultRepository = resultRepository;
    }

    public async Task<int> Handle(GetNumberOfTestResultsForTimeIntervalQuery request, CancellationToken cancellationToken)
    {
        return await _resultRepository.GetNumberOfTestsPerformedInInterval(request.StartTime, request.EndTime);
    }
}