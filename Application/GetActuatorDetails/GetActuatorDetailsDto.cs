using Domain.Entities;

namespace Application.GetActuatorDetails;

public class GetActuatorDetailsDto
{
    public string PCBAId { get; }

    private GetActuatorDetailsDto(string pcbaId)
    {
        PCBAId = pcbaId;
    }
    internal static GetActuatorDetailsDto From(Actuator actuator)
    {
        return new GetActuatorDetailsDto(actuator.PCBAId);
    }
}