namespace BuildingBlocks.Infrastructure.Database.Models;

public class ActuatorPCBAHistoryModel
{
    public int WorkOrderNumber { get; set; }
    public int SerialNumber { get; set; }
    public string PCBAUid { get; set; }
    public ActuatorModel ActuatorModel { get; set; }
    public PCBAModel PCBA { get; set; }
    public DateTime RemovalTime { get; set; }
}