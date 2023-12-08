using Frontend.Util;

namespace Frontend.Model;

public interface IStartUpAmountsModel
{
    public Task<StartUpAmounts> GetStartUpAmounts();
}