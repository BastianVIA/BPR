namespace Frontend.Entities;

public class Actuator
{
    public int WorkOrderNumber { get; init; }
    public int SerialNumber { get; init; }
    public PCBA PCBA  { get; set; }
}