namespace Frontend.Entities;

public class TestResult
{
    public int WorkOrderNumber { get; set; }
    public int SerialNumber { get; set; }
    public string Tester { get; set; }
    public int Bay { get; private set; }
    public string MinServoPosition { get; private set; }
    public string MaxServoPosition { get; private set; }
    public string MinBuslinkPosition { get; private set; }
    public string MaxBuslinkPosition { get; private set; }
    public string ServoStroke { get; private set; }
}