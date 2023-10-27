using BuildingBlocks.Application;

namespace Application.CreatePCBA;

public class CreatePCBACommand : ICommand
{
    internal int PCBAUid { get; }
    
    internal int? ManufacturerNumber { get; }

    private CreatePCBACommand(int pcbaUid, int? manufacturerNo)
    {
        PCBAUid = pcbaUid;
        ManufacturerNumber = manufacturerNo;
    }

    public static CreatePCBACommand Create(int pcbaUid, int? manufacturerNo)
    {
        return new CreatePCBACommand(pcbaUid, manufacturerNo);
    }
}