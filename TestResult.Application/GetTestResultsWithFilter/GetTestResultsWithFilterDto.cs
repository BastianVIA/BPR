namespace TestResult.Application.GetTestResultsWithFilter;

public class GetTestResultsWithFilterDto
{
    public List<TestResultsWithFilterDTO> TestResultDtos { get; }

    private GetTestResultsWithFilterDto()
    {
    }

    public GetTestResultsWithFilterDto(List<TestResultsWithFilterDTO> testResultDtos)
    {
        TestResultDtos = testResultDtos;
    }

    internal static GetTestResultsWithFilterDto From(List<TestResult.Domain.Entities.TestResult> testResult)
    {
        List<TestResultsWithFilterDTO> testResultDtos = new List<TestResultsWithFilterDTO>();
        foreach (var actuatorTest in testResult)
        {
            testResultDtos.Add(TestResultsWithFilterDTO.From(actuatorTest));
        }

        return new GetTestResultsWithFilterDto(testResultDtos);
    }
}

public class TestResultsWithFilterDTO
{
    public int WorkOrderNumber { get; private set; }
    public int SerialNumber { get; private set; }
    public string Tester { get; private set; }
    public int Bay { get; private set; }
    public string? MinServoPosition { get; private set; }
    public string? MaxServoPosition { get; private set; }
    public string? MinBuslinkPosition { get; private set; }
    public string? MaxBuslinkPosition { get; private set; }
    public string? ServoStroke { get; private set; }
    public DateTime TimeOccured { get; private set; }

    

    internal static TestResultsWithFilterDTO From(TestResult.Domain.Entities.TestResult testResult)
    {
        return new TestResultsWithFilterDTO
        {
            WorkOrderNumber = testResult.WorkOrderNumber,
            SerialNumber = testResult.SerialNumber,
            Tester = testResult.Tester,
            Bay = testResult.Bay,
            MinServoPosition = testResult.MinServoPosition,
            MaxServoPosition = testResult.MaxServoPosition,
            MinBuslinkPosition = testResult.MinBuslinkPosition,
            MaxBuslinkPosition = testResult.MaxBuslinkPosition,
            ServoStroke = testResult.ServoStroke,
            TimeOccured = testResult.TimeOccured
        };
    }
}