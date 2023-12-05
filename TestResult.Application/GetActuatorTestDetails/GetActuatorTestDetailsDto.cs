namespace TestResult.Application.GetActuatorTestDetails;

public class GetActuatorTestDetailsDto
{
    public int WorkOrderNumber { get; }
    public int SerialNumber { get; }
    public string Tester { get; set; }
    public int Bay { get; set; }
    public string? MinServoPosition { get; set; }
    public string? MaxServoPosition { get; set; }
    public string? MinBuslinkPosition { get; set; }
    public string? MaxBuslinkPosition { get; set; }
    public string? ServoStroke { get; set; }

    private GetActuatorTestDetailsDto(int workOrderNumber, int serialNumber, string tester, int bay,
        string? minServoPosition, string? maxServoPosition, string? minBuslinkPosition, string? maxBuslinkPosition,
        string? servoStroke)
    {
        WorkOrderNumber = workOrderNumber;
        SerialNumber = serialNumber;
        Tester = tester;
        Bay = bay;
        MinServoPosition = minServoPosition;
        MaxServoPosition = maxServoPosition;
        MinBuslinkPosition = minBuslinkPosition;
        MaxBuslinkPosition = maxBuslinkPosition;
        ServoStroke = servoStroke;
    }

    internal static GetActuatorTestDetailsDto From(Domain.Entities.TestResult actuatorTestResult)
    {
        return new GetActuatorTestDetailsDto(actuatorTestResult.WorkOrderNumber, actuatorTestResult.SerialNumber,
            actuatorTestResult.Tester, actuatorTestResult.Bay, actuatorTestResult.MinServoPosition,
            actuatorTestResult.MaxServoPosition, actuatorTestResult.MinBuslinkPosition,
            actuatorTestResult.MaxBuslinkPosition, actuatorTestResult.ServoStroke);
    }
}