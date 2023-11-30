using Frontend.Model;

namespace Frontend.ServiceExtensions;

public static class ModelServices
{
    public static IServiceCollection AddModels(this IServiceCollection serviceCollection)
    {
       serviceCollection.AddScoped<IActuatorDetailsModel, ActuatorDetailsModel>();
       serviceCollection.AddScoped<IGetActuatorsWithFilterModel, GetActuatorsWithFilterModel>();

       return serviceCollection;
       
    }
}