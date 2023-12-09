using BuildingBlocks.Application;
using TestResult.Domain.Entities;
using TestResult.Domain.RepositoryInterfaces;

namespace TestResult.Application.GetStartUpAmounts;

public class GetTestStartUpAmountsQueryHandler : IQueryHandler<GetTestStartUpAmountsQuery, GetTestStartUpAmountsDto>
{
    private readonly ITestResultRepository _testResultRepository;
    private readonly ITestErrorRepository _testErrorRepository;

    public GetTestStartUpAmountsQueryHandler(ITestResultRepository testResultRepository, ITestErrorRepository testErrorRepository)
    {
        _testResultRepository = testResultRepository;
        _testErrorRepository = testErrorRepository;
    }

    public async Task<GetTestStartUpAmountsDto> Handle(GetTestStartUpAmountsQuery request, CancellationToken cancellationToken)
    {
        var testResultAmount = await _testResultRepository.GetTotalTestResultAmount();
        var testErrorAmount = await _testErrorRepository.GetTotalTestErrorAmounts();
        var testResultWithErrorAmount = await _testResultRepository.GetTotalTestResultWithErrorsAmount();
        var testResultWithoutErrorAmount = await _testResultRepository.GetTotalTestResultWithoutErrorsAmount();
        var startUpAmounts = new TestStartUpAmount(testResultAmount, testErrorAmount, testResultWithErrorAmount,
            testResultWithoutErrorAmount);
        return GetTestStartUpAmountsDto.From(startUpAmounts);
    }
}