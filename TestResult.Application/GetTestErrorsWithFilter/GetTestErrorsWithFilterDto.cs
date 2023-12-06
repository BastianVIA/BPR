using Microsoft.AspNetCore.Http;

namespace TestResult.Application.GetTestErrorsWithFilter;

public class GetTestErrorsWithFilterDto
{
    public List<int> PossibleErrorCodes { get; }
    public List<GetTestErrorsWithFilterSingleLineDto> DataLines { get; }
    public GetTestErrorsWithFilterDto(List<int> possibleErrorCodes, List<GetTestErrorsWithFilterSingleLineDto> dataLines)
    {
        PossibleErrorCodes = possibleErrorCodes;
        DataLines = dataLines;
    }
    public static GetTestErrorsWithFilterDto From(List<int> possibleErrorCodes,
        List<GetTestErrorsWithFilterSingleLineDto> dataLines)
    {
        return new GetTestErrorsWithFilterDto(possibleErrorCodes, dataLines);
    }
}

public class GetTestErrorsWithFilterSingleLineDto
{
    public List<GetTestErrorsWithFilterTestErrorCodeAndAmountDto> ListOfErrors { get; }
    public int TotalErrors { get; }
    public int TotalTests { get; }
    public DateTime StartIntervalAsDate { get; }
    public DateTime EndIntervalAsDate { get; }

    private GetTestErrorsWithFilterSingleLineDto(List<GetTestErrorsWithFilterTestErrorCodeAndAmountDto> listOfErrors, int totalErrors, int totalTests,
        DateTime startIntervalAsDate, DateTime endIntervalAsDate)
    {
        ListOfErrors = listOfErrors;
        TotalErrors = totalErrors;
        TotalTests = totalTests;
        StartIntervalAsDate = startIntervalAsDate;
        EndIntervalAsDate = endIntervalAsDate;
    }

    public static GetTestErrorsWithFilterSingleLineDto From(List<GetTestErrorsWithFilterTestErrorCodeAndAmountDto> listOfErrors,
        int totalErrors, int totalTests,
        DateTime startIntervalAsDate, DateTime endIntervalAsDate)
    {
        return new GetTestErrorsWithFilterSingleLineDto(listOfErrors, totalErrors, totalTests, startIntervalAsDate,
            endIntervalAsDate);
    }
}

public class GetTestErrorsWithFilterTestErrorCodeAndAmountDto
{
    public int ErrorCode { get; set; }
    public string ErrorMessage { get; set; }
    public int AmountOfErrors { get; set; }
}