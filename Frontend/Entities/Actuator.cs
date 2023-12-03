namespace Frontend.Entities;

public class Actuator
{
    public int? WorkOrderNumber { get; set; }
    public int? SerialNumber { get; set; }
    public PCBA PCBA { get; } = new();

    public Actuator WithWorkOrderNumber(int woNo)
    {
        WorkOrderNumber = woNo;
        return this;
    }
    
    public Actuator WithSerialNumber(int serialNo)
    {
        SerialNumber = serialNo;
        return this;
    }

    public Actuator WithPCBAUid(string pcbaUid)
    {
        PCBA.PCBAUid = pcbaUid;
        return this;
    }
    
    public Actuator WithPCBAItemNumber(string itemNo)
    {
        PCBA.ItemNumber = itemNo;
        return this;
    }
    
    public Actuator WithPCBAProductionDateCode(int productionDateCode)
    {
        PCBA.ProductionDateCode = productionDateCode;
        return this;
    }
    
    public Actuator WithPCBAManufacturerNumber(int manufacturerNo)
    {
        PCBA.ManufacturerNumber = manufacturerNo;
        return this;
    }
}
