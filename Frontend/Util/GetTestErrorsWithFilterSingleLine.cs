namespace Frontend.Util;

public class GetTestErrorsWithFilterSingleLine
{
    public DateTime StartIntervalAsDate { get; set; }
    public DateTime EndIntervalAsDate { get; set; }
    public int TotalErrors { get; set; }
    public int TotalTests { get; set; }
    public List<GetTestErrorsWithFilterTestData> ListOfErrors { get; set; } = new();
}