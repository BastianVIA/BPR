namespace BuildingBlocks.Infrastructure.Database.Models;

public class ActuatorModel
{
    public int WorkOrderNumber { get; set; }
    public int SerialNumber { get; set; }
    public string CommunicationProtocol { get; set; }
    public string ArticleNumber { get; set; }
    public DateTime CreatedTime { get; set; }
    public PCBAModel PCBA { get; set; }
    public ArticleModel Article { get; set; }

}