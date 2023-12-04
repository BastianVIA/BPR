using BuildingBlocks.Domain;
using Domain.Events;

namespace Domain.Entities;

public class PCBA : Entity
{
    public string Uid { get; set; }

    public int ManufacturerNumber { get; private set; }
    public string ItemNumber { get; private set; }
    public string Software { get; private set; }
    public int ProductionDateCode { get; private set; }

    private PCBA()
    {
    }

    public PCBA(string uid, int manufacturerNo, string itemNumber, string software, int productionDateCode)
    {
        Uid = uid;
        ManufacturerNumber = manufacturerNo;
        ItemNumber = itemNumber;
        Software = software;
        ProductionDateCode = productionDateCode;
    }

    public PCBA(string uid)
    {
        Uid = uid;
        ManufacturerNumber = 0;
        ItemNumber = "N/A";
        Software = "N/A";
        ProductionDateCode = 0;
    }
}