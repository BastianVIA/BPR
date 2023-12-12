namespace Frontend.Model;

public interface IUpdateActuatorsPCBAModel
{
    Task UpdateActuatorsPCBA(int woNo, int serialNo, string pcbaUid);
}