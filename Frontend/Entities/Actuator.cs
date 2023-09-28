using System.ComponentModel.DataAnnotations;

namespace Frontend.Entities;

public class Actuator
{
    public int WorkOrderNumber { get; set; }
    public int SerialNumber { get; set; }
    public PCBA PCBA  { get; set; }
}