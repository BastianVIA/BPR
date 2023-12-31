namespace BuildingBlocks.Infrastructure.Database.Models;

public class PCBAModel
{
    public string Uid { get; set; }
    public int ManufacturerNumber { get; set; }
    public string ItemNumber { get; set; }
    public string Software { get; set; }
    public int ProductionDateCode { get; set; }
    public string ConfigNo { get; set; }
}