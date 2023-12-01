namespace Frontend.Entities;

[Serializable]
public class PCBA : Component
{
    public string PCBAUid { get; set; }
    public int ManufacturerNumber { get; set; }
    
    public int ItemNumber { get; set; }
    
    public int ProductionDateCode { get; set; }
}