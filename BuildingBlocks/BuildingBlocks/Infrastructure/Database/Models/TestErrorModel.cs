namespace Infrastructure;

public class TestErrorModel
{
    public Guid Id { get; set; }
    public Guid TestResultId { get; set; }
    public string Tester { get; set; }
    public int Bay { get; set; }
    public int ErrorCode { get; set; }
    public string ErrorMessage { get; set; }
    public DateTime TimeOccured { get; set; }
}