using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Model;

public class PCBAModel : ComponentModel
{
    
    public int PCBAUid { get; set; } //PK
    
    public ActuatorModel Actuator { get; set; } 

    // public int ManufacturerNumber { get; set; }
    //
    // public string CommunicationProtocol { get; set; }
    //
    // public int WorkOrderNumber { get; set; }
    //
    // public int ProductionDateCode { get; set; }
    //
    // public string RevisionNumber { get; set; }
    //
    // public string Software { get; set; }
    //
    // public string ConfigNumber { get; set; }

}