using BuildingBlocks.Domain;
using Domain.Events;

namespace Domain.Entities;

public class PCBA : Entity
{
    public string Uid { get; private set; }

    public int ManufacturerNumber { get; private set; }

    private PCBA()
    {
    }

    public PCBA(string uid, int manufacturerNo)
    {
        Uid = uid;
        ManufacturerNumber = manufacturerNo;
    }
}