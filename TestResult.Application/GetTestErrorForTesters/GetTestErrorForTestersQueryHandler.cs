using BuildingBlocks.Application;
using TestResult.Domain.Repositories;

namespace TestResult.Application.GetTestErrorForTesters;

public class GetTestErrorForTestersQueryHandler : IQueryHandler<GetTestErrorForTestersQuery, GetTestErrorForTestersDto>
{
    private readonly ITestErrorRepository _errorRepository;

    public GetTestErrorForTestersQueryHandler(ITestErrorRepository errorRepository)
    {
        _errorRepository = errorRepository;
    }

    public async Task<GetTestErrorForTestersDto> Handle(GetTestErrorForTestersQuery request,
        CancellationToken cancellationToken)
    {
        var (startDate, endDate, intervalInMinutes) =
            TesterTimePeriodEnumMapper.MapEnumToDateAndIntervalInMinutes(request.TimePeriod);

        var errorsForTesters = await _errorRepository.GetAllErrorsForTesterSince(request.Testers, startDate, endDate);


        Dictionary<string, List<GetTestErrorForTestersErrorDto>> testerToErrors =
            request.Testers.ToDictionary(tester => tester, tester => new List<GetTestErrorForTestersErrorDto>());

        do
        {
            var endOfInterval = startDate + TimeSpan.FromMinutes(intervalInMinutes);

            var errorsForInterval = errorsForTesters
                .Where(error => error.TimeOccured >= startDate && error.TimeOccured < endOfInterval)
                .ToList();

            Dictionary<string, int> testerToErrorCount = new();
            foreach (var testError in errorsForInterval)
            {
                if (testerToErrorCount.ContainsKey(testError.Tester))
                {
                    testerToErrorCount[testError.Tester] += 1;
                }
                else
                {
                    testerToErrorCount[testError.Tester] = 1;
                }
            }

            foreach (var tester in request.Testers)
            {
                var numberOfErrorsInTimeInterval = 0;
                if (testerToErrorCount.ContainsKey(tester))
                {
                    numberOfErrorsInTimeInterval = testerToErrorCount[tester];
                }

                testerToErrors[tester]
                    .Add(GetTestErrorForTestersErrorDto.From(startDate, numberOfErrorsInTimeInterval));
            }
            startDate = endOfInterval;
        } while (startDate < endDate);


        List<GetTestErrorForTestersTesterDto> testersWithErrors = testerToErrors
            .Select(kv => new GetTestErrorForTestersTesterDto
            {
                Name = kv.Key,
                Errors = kv.Value
            })
            .ToList();

        return GetTestErrorForTestersDto.From(testersWithErrors);
    }
}