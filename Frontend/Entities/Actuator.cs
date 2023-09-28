namespace Frontend.Entities;

public class Actuator
{
    public string PcbaId { get; }

    public Actuator(string pcbaId)
    {
        PcbaId = pcbaId;
    }
}