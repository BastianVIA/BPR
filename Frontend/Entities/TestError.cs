namespace Frontend.Entities;

public class TestError
{
    public string Tester { get; set; }
    public int Bay { get; set; }
    public int ErrorCode { get; set; }
    public string ErrorMessage { get; set; }
    public DateTime TimeOccured { get; set; }
    
}

public class TestError
{
    public List<GetTestErrorsWithFilterErrorCodeAndMessage> PossibleErrorCodes { get; set; } = new();
    public List<GetTestErrorsWithFilterSingleLine> DataLines { get; set; } = new();

    public TestError()
    {
    }
}

public class GetTestErrorsWithFilterErrorCodeAndMessage
{
    public int ErrorCode { get; set; }
    public string ErrorMessage { get; set; }
}

public class GetTestErrorsWithFilterSingleLine
{
    public DateTime StartIntervalAsDate { get; set; }
    public DateTime EndIntervalAsDate { get; set; }
    public int TotalErrors { get; set; }

    public int TotalTests { get; set; }

    public List<GetTestErrorsWithFilterTestData> listOfErrors { get; set; } = new();
}

public class GetTestErrorsWithFilterTestData
{
    public int ErrorCode { get; set; }
    public int AmountOfErrors { get; set; }
}