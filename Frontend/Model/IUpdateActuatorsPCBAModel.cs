namespace Frontend.Model;

public interface IUpdateActuatorsPCBAModel
{
    public Task UpdateActuatorsPCBA(int woNo, int serialNo, string pcbaUid);
}