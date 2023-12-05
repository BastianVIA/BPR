using BuildingBlocks.Application;

namespace Application.CreatePCBA;

public class CreatePCBACommand : ICommand
{
    public string Uid { get; private set; }

    public int ManufacturerNumber { get; private set; }
    public string ItemNumber { get; private set; }
    public string Software { get; private set; }
    public int ProductionDateCode { get; private set; }
    public string ConfigNo { get; private set; }

    private CreatePCBACommand(string pcbaUid, int manufacturerNo, string itemNumber, string software,
        int productionDateCode, string configNo)
    {
        Uid = pcbaUid;
        ManufacturerNumber = manufacturerNo;
        ItemNumber = itemNumber;
        Software = software;
        ProductionDateCode = productionDateCode;
        ConfigNo = configNo;
    }

    public static CreatePCBACommand Create(string pcbaUid, int manufacturerNo, string itemNumber, string software,
        int productionDateCode, string configNo)
    {
        return new CreatePCBACommand(pcbaUid, manufacturerNo, itemNumber, software, productionDateCode, configNo);
    }
}