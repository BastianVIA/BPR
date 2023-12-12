namespace Frontend.Util;

public class TestErrorResponse
{
    public List<GetTestErrorsWithFilterErrorCodeAndMessage> PossibleErrorCodes { get; set; } = new();
    public List<GetTestErrorsWithFilterSingleLine> DataLines { get; set; } = new();

    public TestErrorResponse()
    {
    }
}