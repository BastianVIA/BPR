using Frontend.Model;

namespace Frontend.ServiceExtensions;

public static class ModelServices
{
    public static IServiceCollection AddModels(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IActuatorDetailsModel, ActuatorDetailsModel>();
        serviceCollection.AddScoped<IActuatorSearchModel, ActuatorSearchModel>();
        serviceCollection.AddScoped<IErrorStatisticsModel, ErrorStatisticsModel>();
        serviceCollection.AddScoped<ITestResultSearchModel, TestResultSearchModelModel>();
        serviceCollection.AddScoped<IActuatorSearchCsvModel, ActuatorSearchCsvModel>();
        serviceCollection.AddScoped<IUpdateActuatorsPCBAModel, UpdateActuatorsPCBAModel>();
        serviceCollection.AddScoped<IActuatorComponentHistoryModel, ActuatorComponentHistoryModel>();
        serviceCollection.AddScoped<ITestErrorsSearchModel, TestErrorsSearchModel>();
        serviceCollection.AddScoped<IStartUpAmountsModel, StartUpAmountsModel>();

        return serviceCollection;
    }
}