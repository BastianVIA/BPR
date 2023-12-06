namespace Infrastructure;

public class TestResultModel
{
    public Guid Id { get; set; }
    public int WorkOrderNumber { get; set; }
    public int SerialNumber { get; set; }
    public string Tester { get; set; }
    public int Bay { get; set; }
    public string MinServoPosition { get; set; } = "N/A";
    public string MaxServoPosition { get; set; } = "N/A";
    public string MinBuslinkPosition { get; set; } = "N/A";
    public string MaxBuslinkPosition { get; set; } = "N/A";
    public string ServoStroke { get; set; } = "N/A";
    public DateTime TimeOccured { get; set; }
    public List<TestErrorModel> TestErrors { get; set; } = new();
}