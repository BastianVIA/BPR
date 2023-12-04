using BuildingBlocks.Application;
using Domain.Entities;

namespace Application.CreateActuator;

public class CreateActuatorCommand : ICommand
{
    public int WorkOrderNumber { get; private set; }
    public int SerialNumber { get; private set; }
    public string PCBAUid { get; private set; }
    public string CommunicationProtocol { get; private set; }
    public string ArticleNumber { get; private set; }
    public string ArticleName { get; private set; }
    public DateTime CreatedTime { get; private set; }

    private CreateActuatorCommand() { }

    private CreateActuatorCommand(int woNo, int serialNo, string pcbaUid, string articleNo,
        string articleName, string communicationProtocol, DateTime createdTime)
    {
        WorkOrderNumber = woNo;
        SerialNumber = serialNo;
        PCBAUid = pcbaUid;
        ArticleNumber = articleNo;
        ArticleName = articleName;
        CommunicationProtocol = communicationProtocol;
        CreatedTime = createdTime;
    }

    public static CreateActuatorCommand Create(int woNo, int serialNo, string pcbaUid, string articleNo,
        string articleName, string communicationProtocol, DateTime createdTime)
    {
        return new CreateActuatorCommand(woNo, serialNo, pcbaUid, articleNo, articleName, communicationProtocol, createdTime);
    }
}