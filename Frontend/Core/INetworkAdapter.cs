namespace Frontend.Core;

public interface INetworkAdapter
{
    Task<T> GetPCBAAsync<T>();
}