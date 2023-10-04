namespace Domain.Entities;

public class CompositeActuatorId
{
    public int WorkOrderNumber { get; }
    public int SerialNumber { get; }

    private CompositeActuatorId()
    {
    }
    
    private CompositeActuatorId(int woNo, int serialNo)
    {
        WorkOrderNumber = woNo;
        SerialNumber = serialNo;
    }

    public static CompositeActuatorId From(int woNo, int serialNo)
    {
        //put validation , fx WoNo == 8 lang.
        return new CompositeActuatorId(woNo, serialNo);
    }
    
}