using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure;

public class ActuatorModel
{
    [Key]
    public int WorkOrderNumber { get; set; }

    [Key]
    public int SerialNumber { get; set; }
    
    public PCBAModel PCBA { get; set; }
}