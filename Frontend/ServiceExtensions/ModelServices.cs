using Frontend.Model;

namespace Frontend.ServiceExtensions;

public static class ModelServices
{
    public static IServiceCollection AddModels(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IActuatorDetailsModel, ActuatorDetailsModel>();
        serviceCollection.AddScoped<IActuatorSearchModel, ActuatorSearchModel>();
        serviceCollection.AddScoped<ITestErrorModel, TestErrorModel>();
        serviceCollection.AddScoped<ITestResultSearchModel, TestResultSearchModelModel>();
        serviceCollection.AddScoped<IActuatorSearchCsvModel, ActuatorSearchCsvModel>();
        
        return serviceCollection;
    }
}