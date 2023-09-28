namespace Domain.Entities;

public class CompositeActuatorId
{
    public string WONo { get; }
    public string SerialNo { get; }

    private CompositeActuatorId()
    {
    }
    
    private CompositeActuatorId(string woNo, string serialNo)
    {
        WONo = woNo;
        SerialNo = serialNo;
    }

    public static CompositeActuatorId From(string WONo, string SerialNo)
    {
        if (string.IsNullOrEmpty(WONo) || string.IsNullOrEmpty(SerialNo))
        {
            throw new ArgumentException($"{nameof(WONo)} or {nameof(SerialNo)} not valid");
        }

        return new CompositeActuatorId(WONo, SerialNo);
    }
    
}