namespace Domain.Entities;

public class CompositeActuatorId
{
    public int WONo { get; }
    public int SerialNo { get; }

    private CompositeActuatorId()
    {
    }
    
    private CompositeActuatorId(int woNo, int serialNo)
    {
        WONo = woNo;
        SerialNo = serialNo;
    }

    public static CompositeActuatorId From(int woNo, int serialNo)
    {
        return new CompositeActuatorId(woNo, serialNo);
    }
    
}