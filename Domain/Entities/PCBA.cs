using BuildingBlocks.Domain;
using Domain.Events;

namespace Domain.Entities;

public class PCBA : Entity
{
    public int PCBAUid { get; private set; }

    public int? ManufacturerNumber { get; private set; }

    private PCBA()
    {
    }

    public PCBA(int pcbaUid, int? manufacturerNo)
    {
        PCBAUid = pcbaUid;
        ManufacturerNumber = manufacturerNo;
    }

    public static PCBA Create(int pcbaUid, int? manufacturerNo)
    {
        var pcba = new PCBA(pcbaUid, manufacturerNo);
        pcba.AddDomainEvent(new PCBACreatedDomainEvent(pcbaUid));
        return pcba;
    }
}