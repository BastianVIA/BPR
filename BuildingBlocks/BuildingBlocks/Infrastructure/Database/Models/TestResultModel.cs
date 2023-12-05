namespace Infrastructure;

public class TestResultModel
{
    public Guid Id { get; set; }
    public int WorkOrderNumber { get; set; }
    public int SerialNumber { get; set; }
    public string Tester { get; set; }
    public int Bay { get; set; }
    public string? MinServoPosition { get; set; }
    public string? MaxServoPosition { get; set; }
    public string? MinBuslinkPosition { get; set; }
    public string? MaxBuslinkPosition { get; set; }
    public string? ServoStroke { get; set; }
    public DateTime TimeOccured { get; set; }
    public List<TestErrorModel>? TestErrors { get; set; }
}