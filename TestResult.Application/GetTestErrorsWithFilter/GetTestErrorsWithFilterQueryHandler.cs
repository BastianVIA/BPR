using BuildingBlocks.Application;
using TestResult.Application.GetNumberOfTestResultsForTimeInterval;
using TestResult.Domain.Entities;
using TestResult.Domain.Repositories;

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
        var errorsMatchingFilter = await _errorRepository.GetTestErrorsWithFilter(request.WorkOrderNumber,
            request.Tester, request.Bay,
            request.ErrorCode, request.StartDate, request.EndDate);

        HashSet<int> uniqueErrorCodes = new HashSet<int>();
        List<GetTestErrorsWithFilterSingleLineDto> dataLines = new();

        var startOfInterval = request.StartDate;
        do
        {
            var endOfInterval = startOfInterval + TimeSpan.FromMinutes(request.TimeIntervalBetweenRowsAsMinutes);
            
            var numberOfTestResultsForInterval = GetNumberOfTestResultsForInterval(cancellationToken, startOfInterval, endOfInterval);

            var intervalErrors = errorsMatchingFilter
                .Select(error => error)
                .Where(error => error.TimeOccured >= startOfInterval && error.TimeOccured < endOfInterval)
                .ToList();
            
            var singleLine = await CreateSingleLine(intervalErrors, startOfInterval, endOfInterval, uniqueErrorCodes,
                numberOfTestResultsForInterval);
            
            dataLines.Add(singleLine);
            
            startOfInterval = endOfInterval;
            
        } while (startOfInterval < request.EndDate);
        
        List<int> possibleErrorCodes = uniqueErrorCodes.ToList();
        return GetTestErrorsWithFilterDto.From(possibleErrorCodes, dataLines);
    }

    private async Task<GetTestErrorsWithFilterSingleLineDto> CreateSingleLine(List<TestError> errors,
        DateTime startIntervalAsDate,
        DateTime endIntervalAsDate, HashSet<int> uniqueErrorCodes, Task<int> numberOfTestResultsForInterval)
    {
        Dictionary<int, int> testErrorsAndAmount = new();
        Dictionary<int, string> errorCodeToMessage = new();
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
                ErrorMessage = errorCodeToMessage[kv.Key],
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