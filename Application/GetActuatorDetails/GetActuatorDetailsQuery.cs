using BuildingBlocks.Application;

namespace Application.GetActuatorDetails;

public class GetActuatorDetailsQuery : IQuery<GetActuatorDetailsDto>
{
    internal string WONo { get; }
    internal string SerialNo { get; }

    private GetActuatorDetailsQuery(string woNo, string serialNo)
    {
        WONo = woNo;
        SerialNo = serialNo;
    }

    public static GetActuatorDetailsQuery Create(string WONo, string SerialNo)
    {
        return new GetActuatorDetailsQuery(WONo, SerialNo);
    }
}