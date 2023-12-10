using BuildingBlocks.Application;

namespace Application.CreatePCBA;

public class CreateOrUpdatePCBACommand : ICommand
{
    public string Uid { get; private set; }

    public int ManufacturerNumber { get; private set; }
    public string ItemNumber { get; private set; }
    public string Software { get; private set; }
    public int ProductionDateCode { get; private set; }
    public string ConfigNo { get; private set; }

    private CreateOrUpdatePCBACommand(string pcbaUid, int manufacturerNo, string itemNumber, string software,
        int productionDateCode, string configNo)
    {
        Uid = pcbaUid;
        ManufacturerNumber = manufacturerNo;
        ItemNumber = itemNumber;
        Software = software;
        ProductionDateCode = productionDateCode;
        ConfigNo = configNo;
    }

    public static CreateOrUpdatePCBACommand Create(string pcbaUid, int manufacturerNo, string itemNumber, string software,
        int productionDateCode, string configNo)
    {
        return new CreateOrUpdatePCBACommand(pcbaUid, manufacturerNo, itemNumber, software, productionDateCode, configNo);
    }
}