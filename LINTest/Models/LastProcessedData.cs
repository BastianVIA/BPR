namespace LINTest.Models;

[Serializable]
public class LastProcessedData
{
    public DateTime LastProcessedTime { get; set; }
    public int ConsecutiveFails { get; set; }

    public LastProcessedData(DateTime lastProcessedTime, int consecutiveFails = 0)
    {
        LastProcessedTime = lastProcessedTime;
        ConsecutiveFails = consecutiveFails;
    }
}