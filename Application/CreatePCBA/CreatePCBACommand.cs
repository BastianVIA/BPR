using BuildingBlocks.Application;

namespace Application.CreatePCBA;

public class CreatePCBACommand : ICommand
{
    internal string Uid { get; }
    
    internal int ManufacturerNumber { get; }

    private CreatePCBACommand(string pcbaUid, int manufacturerNo)
    {
        Uid = pcbaUid;
        ManufacturerNumber = manufacturerNo;
    }

    public static CreatePCBACommand Create(string pcbaUid, int manufacturerNo)
    {
        return new CreatePCBACommand(pcbaUid, manufacturerNo);
    }
}