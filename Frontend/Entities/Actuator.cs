namespace Frontend.Entities;

public class Actuator
{
    public int WorkOrderNumber { get; private set; }
    public int SerialNumber { get; private set; }
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

    public Actuator WithPCBAUid(int pcbaUid)
    {
        PCBA.PCBAUid = pcbaUid;
        return this;
    }
}
