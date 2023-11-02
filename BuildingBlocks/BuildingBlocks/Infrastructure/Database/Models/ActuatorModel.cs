using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure;

public class ActuatorModel
{
    public int WorkOrderNumber { get; set; }
    
    public int SerialNumber { get; set; }
    
    public PCBAModel PCBA { get; set; }
}