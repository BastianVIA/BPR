using Frontend.Util;

namespace Frontend.Model;

public interface IStartUpAmountsModel
{
    Task<StartUpAmounts> GetStartUpAmounts();
}