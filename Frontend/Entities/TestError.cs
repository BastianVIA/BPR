namespace Frontend.Entities;

public class TestError
{
    public string Tester { get; set; }
    public int Bay { get; set; }
    public int ErrorCode { get; set; }
    public string ErrorMessage { get; set; }
    public DateTime TimeOccured { get; set; }
}