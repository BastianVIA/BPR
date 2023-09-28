using Domain.Entities;

namespace Application.GetActuatorDetails;

public class GetActuatorDetailsDto
{
    public int PCBAUId { get; }

    private GetActuatorDetailsDto(int PCBAUid)
    {
        PCBAUId = PCBAUid;
    }
    internal static GetActuatorDetailsDto From(Actuator actuator)
    {
        return new GetActuatorDetailsDto(actuator.PCBAUId);
    }
}