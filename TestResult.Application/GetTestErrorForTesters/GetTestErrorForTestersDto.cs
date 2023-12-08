namespace TestResult.Application.GetTestErrorForTesters;

public class GetTestErrorForTestersDto
{
    public List<GetTestErrorForTestersTesterDto> ErrorsForTesters { get;  }

    private GetTestErrorForTestersDto() { }
    private GetTestErrorForTestersDto(List<GetTestErrorForTestersTesterDto> errorsForTesters)
    {
        ErrorsForTesters = errorsForTesters;
    }

    public static GetTestErrorForTestersDto From(List<GetTestErrorForTestersTesterDto> errorsForTesters)
    {
        
        return new GetTestErrorForTestersDto(errorsForTesters);
    }
}

public class GetTestErrorForTestersTesterDto
{
    public string Name { get; set; }
    public List<GetTestErrorForTestersErrorDto> Errors { get; set; }
    public GetTestErrorForTestersTesterDto(){} 
    private GetTestErrorForTestersTesterDto(string name, List<GetTestErrorForTestersErrorDto> errors)
    {
        Name = name;
        Errors = errors;
    }

    public static GetTestErrorForTestersTesterDto From(string name, List<GetTestErrorForTestersErrorDto> dtos)
    {
        List<GetTestErrorForTestersErrorDto> errors = new();
        foreach (var dto in dtos)
        {
            errors.Add(GetTestErrorForTestersErrorDto.From(dto.Date, dto.ErrorCount));
        }
        return new GetTestErrorForTestersTesterDto(name, errors);
    }
   
}

public class GetTestErrorForTestersErrorDto
{
    public DateTime Date { get; }
    public int ErrorCount { get; }
    private GetTestErrorForTestersErrorDto(){}
    private GetTestErrorForTestersErrorDto(DateTime date, int errorCount)
    {
        Date = date;
        ErrorCount = errorCount;
    }

    public static GetTestErrorForTestersErrorDto From(DateTime date, int errorCount)
    {
        return new GetTestErrorForTestersErrorDto(date, errorCount);
    }
}