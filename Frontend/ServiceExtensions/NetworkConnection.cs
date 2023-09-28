
using Frontend.Core;

namespace Frontend.ServiceExtensions;

public static class NetworkConnection
{
    public static IServiceCollection SetupBackendConnection(this IServiceCollection services)
    {
        
        services.AddHttpClient();
        services.AddSingleton<INetworkAdapter, NSwagAdapter>();
        
        return services;
    }
}