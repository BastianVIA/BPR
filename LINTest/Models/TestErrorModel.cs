namespace LINTest.Models;

public class TestErrorModel
{
    public int WorkOrderNumber { get; set; }
    public int SerialNumber { get; set; }
    public string Tester { get; set; }
    public int Bay { get; set; }
    public int ErrorCode { get; set; }
    public string ErrorMessage { get; set; }
    public DateTime TimeOccured { get; set; }
}