namespace Frontend.Entities;

[Serializable]
public class PCBA
{
    public string? PCBAUid { get; set; }
    public string? ItemNumber { get; set; }
    public int? ManufacturerNumber { get; set; }
    public int? ProductionDateCode { get; set; }
    public string? Software { get; set; }
    public string? ConfigNumber { get; set; }
}