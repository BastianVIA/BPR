using System.Diagnostics;
using BuildingBlocks.Application;
using TestResult.Application.GetNumberOfTestResultsForTimeInterval;
using TestResult.Domain.Entities;
using TestResult.Domain.RepositoryInterfaces;

namespace TestResult.Application.GetTestErrorsWithFilter;

public class
    GetTestErrorsWithFilterQueryHandler : IQueryHandler<GetTestErrorsWithFilterQuery, GetTestErrorsWithFilterDto>
{
    private readonly ITestErrorRepository _errorRepository;
    private readonly IQueryBus _bus;

    public GetTestErrorsWithFilterQueryHandler(ITestErrorRepository errorRepository, IQueryBus bus)
    {
        _errorRepository = errorRepository;
        _bus = bus;
    }

    public async Task<GetTestErrorsWithFilterDto> Handle(GetTestErrorsWithFilterQuery request,
        CancellationToken cancellationToken)
    {
        var timer = new Stopwatch();
        timer.Start();
        var errorsMatchingFilter = await _errorRepository.GetTestErrorsWithFilter(request.WorkOrderNumber,
            request.Tester, request.Bay,
            request.ErrorCode, request.StartDate, request.EndDate);
        
        timer.Stop();
        Console.WriteLine("------------------- Time elapsed: " + timer.Elapsed);
        HashSet<int> uniqueErrorCodes = new HashSet<int>();
        Dictionary<int, string> errorCodeToMessage = new();

        List<GetTestErrorsWithFilterSingleLineDto> dataLines = new();

        var startOfInterval = request.StartDate;
        do
        {
            var endOfInterval = startOfInterval + TimeSpan.FromMinutes(request.TimeIntervalBetweenRowsAsMinutes);

            var numberOfTestResultsForInterval =
                GetNumberOfTestResultsForInterval(cancellationToken, startOfInterval, endOfInterval);

            var intervalErrors = errorsMatchingFilter
                .Where(error => error.TimeOccured >= startOfInterval && error.TimeOccured < endOfInterval)
                .ToList();

            var singleLine = await CreateSingleLine(intervalErrors, startOfInterval, endOfInterval, uniqueErrorCodes,
                numberOfTestResultsForInterval, errorCodeToMessage);

            dataLines.Add(singleLine);

            startOfInterval = endOfInterval;
        } while (startOfInterval <= (request.EndDate ?? DateTime.Now));

        List<int> possibleErrorCodes = uniqueErrorCodes.ToList();

        List<GetTestErrorsWithFilterErrorCodeAndMessageDto> dtoErrorCodes = possibleErrorCodes.Select(errorCode =>
            GetTestErrorsWithFilterErrorCodeAndMessageDto.From(errorCode, errorCodeToMessage[errorCode])
        ).ToList();

        return GetTestErrorsWithFilterDto.From(dtoErrorCodes, dataLines);
    }

    private async Task<GetTestErrorsWithFilterSingleLineDto> CreateSingleLine(List<TestError> errors,
        DateTime startIntervalAsDate,
        DateTime endIntervalAsDate, HashSet<int> uniqueErrorCodes, Task<int> numberOfTestResultsForInterval,
        Dictionary<int, string> errorCodeToMessage)
    {
        Dictionary<int, int> testErrorsAndAmount = new();
        foreach (var testError in errors)
        {
            uniqueErrorCodes.Add(testError.ErrorCode);

            if (testErrorsAndAmount.ContainsKey(testError.ErrorCode))
            {
                testErrorsAndAmount[testError.ErrorCode] += 1;
            }
            else
            {
                testErrorsAndAmount[testError.ErrorCode] = 1;
                errorCodeToMessage[testError.ErrorCode] = testError.ErrorMessage;
            }
        }


        List<GetTestErrorsWithFilterTestErrorCodeAndAmountDto> listOfErrors = testErrorsAndAmount
            .Select(kv => new GetTestErrorsWithFilterTestErrorCodeAndAmountDto
            {
                ErrorCode = kv.Key,
                AmountOfErrors = kv.Value
            })
            .ToList();

        var totalErrorsForInterval = testErrorsAndAmount.Values.Sum();

        var totalNumberOfTestsForInterval = totalErrorsForInterval + await numberOfTestResultsForInterval;

        return GetTestErrorsWithFilterSingleLineDto.From(listOfErrors, totalErrorsForInterval,
            totalNumberOfTestsForInterval, startIntervalAsDate,
            endIntervalAsDate);
    }

    private Task<int> GetNumberOfTestResultsForInterval(CancellationToken cancellationToken, DateTime startOfInterval,
        DateTime endOfInterval)
    {
        var numberOfTestResultsForInterval = _bus.Send(
            GetNumberOfTestResultsForTimeIntervalQuery.From(startOfInterval, endOfInterval),
            cancellationToken);
        return numberOfTestResultsForInterval;
    }
}