public class GetActuatorTestDetailsDto
{
    public List<ActuatorTestDetailDTO> ActuatorTestDetailDtos { get; }

    private GetActuatorTestDetailsDto()
    {
    }

    public GetActuatorTestDetailsDto(List<ActuatorTestDetailDTO> actuatorTestDetailDtos)
    {
        ActuatorTestDetailDtos = actuatorTestDetailDtos;
    }

    internal static GetActuatorTestDetailsDto From(List<TestResult.Domain.Entities.TestResult> actuatorTestResult)
    {
        List<ActuatorTestDetailDTO> actuatorTestDetailDtos = new List<ActuatorTestDetailDTO>();
        foreach (var actuatorTest in actuatorTestResult)
        {
            actuatorTestDetailDtos.Add(ActuatorTestDetailDTO.From(actuatorTest));
        }

        return new GetActuatorTestDetailsDto(actuatorTestDetailDtos);
    }
}

public class ActuatorTestDetailDTO
{
    public int? WorkOrderNumber { get; private set; }
    public int? SerialNumber { get; private set; }
    public string? Tester { get; private set; }
    public int? Bay { get; private set; }
    public string? MinServoPosition { get; private set; }
    public string? MaxServoPosition { get; private set; }
    public string? MinBuslinkPosition { get; private set; }
    public string? MaxBuslinkPosition { get; private set; }
    public string? ServoStroke { get; private set; }

    internal static ActuatorTestDetailDTO From(TestResult.Domain.Entities.TestResult actuatorTest)
    {
        return new ActuatorTestDetailDTO
        {
            WorkOrderNumber = actuatorTest.WorkOrderNumber,
            SerialNumber = actuatorTest.SerialNumber,
            Tester = actuatorTest.Tester,
            Bay = actuatorTest.Bay,
            MinServoPosition = actuatorTest.MinServoPosition,
            MaxServoPosition = actuatorTest.MaxServoPosition,
            MinBuslinkPosition = actuatorTest.MinBuslinkPosition,
            MaxBuslinkPosition = actuatorTest.MaxBuslinkPosition,
            ServoStroke = actuatorTest.ServoStroke
        };
    }
}