using Frontend.Networking;

namespace Frontend.ServiceExtensions;

public static class NetworkConnection
{
    public static IServiceCollection SetupBackendConnection(this IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddSingleton<INetwork, NSwagProxy>();
        
        return services;
    }
}