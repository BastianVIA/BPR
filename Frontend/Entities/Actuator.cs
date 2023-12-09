namespace Frontend.Entities;

public class Actuator
{
    public int WorkOrderNumber { get; set; }
    public int SerialNumber { get; set; }
    public string? ArticleNumber { get; set; }
    public string? ArticleName { get; set; }
    public string? CommunicationProtocol { get; set; }
    public DateTime? CreatedTime { get; set; }
    public PCBA PCBA { get; } = new();


    // Actuator
    public Actuator WithWorkOrderNumber(int woNo)
    {
        WorkOrderNumber = woNo;
        return this;
    }

    public Actuator WithSerialNumber(int serialNo)
    {
        SerialNumber = serialNo;
        return this;
    }

    public Actuator WithArticleNumber(string articleNo)
    {
        ArticleNumber = articleNo;
        return this;
    }

    public Actuator WithArticleName(string articleName)
    {
        ArticleName = articleName;
        return this;
    }

    public Actuator WithCommunicationProtocol(string comProtocol)
    {
        CommunicationProtocol = comProtocol;
        return this;
    }

    public Actuator WithCreatedTime(DateTime createdTime)
    {
        CreatedTime = createdTime;
        return this;
    }
    
    // PCBA

    public Actuator WithSoftware(string software)
    {
        PCBA.Software = software;
        return this;
    }

    public Actuator WithConfigNumber(string configNo)
    {
        PCBA.ConfigNumber = configNo;
        return this;
    }

    public Actuator WithPCBAUid(string pcbaUid)
    {
        PCBA.PCBAUid = pcbaUid;
        return this;
    }

    public Actuator WithPCBAItemNumber(string itemNo)
    {
        PCBA.ItemNumber = itemNo;
        return this;
    }

    public Actuator WithPCBAProductionDateCode(int productionDateCode)
    {
        PCBA.ProductionDateCode = productionDateCode;
        return this;
    }

    public Actuator WithPCBAManufacturerNumber(int manufacturerNo)
    {
        PCBA.ManufacturerNumber = manufacturerNo;
        return this;
    }
}