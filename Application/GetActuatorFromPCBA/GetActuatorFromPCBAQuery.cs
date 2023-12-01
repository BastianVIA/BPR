using BuildingBlocks.Application;

namespace Application.GetActuatorFromPCBA;

public class GetActuatorFromPCBAQuery : IQuery<GetActuatorFromPCBADto>
{
    internal string Uid { get; }
    internal int? ManufacturerNo { get; }
    
    private GetActuatorFromPCBAQuery(){}

    private GetActuatorFromPCBAQuery(string uid, int? manufacturerNo)
    {
        Uid = uid;
        ManufacturerNo = manufacturerNo;
    }

    public static GetActuatorFromPCBAQuery Create(string uid, int? manufacturerNo)
    {
        return new GetActuatorFromPCBAQuery(uid, manufacturerNo);
    }
}