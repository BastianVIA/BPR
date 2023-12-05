using Frontend.Entities;
using Frontend.Exceptions;
using Frontend.Model;
using Frontend.Service.AlertService;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace Frontend.Pages;

public class ActuatorSearchBase : ComponentBase
{
    [Inject] public IActuatorSearchModel SearchModel { get; set; }

    [Inject] public IActuatorSearchCsvModel SearchCsvModel { get; set; }
    
    [Inject] public IActuatorDetailsModel DetailsModel { get; set; }

    [Inject] public IAlertService AlertService { get; set; }

    [Inject] public DialogService DialogService { get; set; }
    
    [Inject] public NavigationManager NavigationManager { get; set; }

    // Radzen needs a class to specify the data object
    public Actuator SearchActuator { get; } = new();

    public List<Actuator> actuators = new();

    // Blazor page needs an empty constructor
    public ActuatorSearchBase()
    {
    }

    // Overloaded constructor for unit testing
    public ActuatorSearchBase(IActuatorSearchModel model)
    {
        SearchModel = model;
    }

    public async Task SearchActuators()
    {
        try
        {
            actuators = await SearchModel.GetActuatorWithFilter(
                SearchActuator.WorkOrderNumber,
                SearchActuator.SerialNumber,
                SearchActuator.PCBA.PCBAUid,
                SearchActuator.PCBA.ItemNumber,
                SearchActuator.PCBA.ManufacturerNumber,
                SearchActuator.PCBA.ProductionDateCode,
                SearchActuator.CreatedTimeStart,
                SearchActuator.CreatedTimeEnd,
                SearchActuator.PCBA.Software,
                SearchActuator.PCBA.ConfigNumber,
                SearchActuator.ArticleName,
                SearchActuator.ArticleNumber,
                SearchActuator.CommunicationProtocol
            );
        }
        catch (NetworkException e)
        {
            AlertService.FireEvent(AlertStyle.Danger, e.Message);
            actuators = new List<Actuator>();
        }
    }

    public async Task ShowActuatorDetails(Actuator actuator)
    {
        await DialogService.OpenAsync<InformationContainer>($"Details",
            new Dictionary<string, object>() { { "Actuator", actuator } },
            new DialogOptions() { Width = "700px", Height = "530px", Resizable = true, Draggable = true });
    }

    public async Task DownloadActuators(List<string> selectedFilter)
    {
        var file = await SearchCsvModel.GetActuatorWithFilter(selectedFilter,
            SearchActuator.WorkOrderNumber,
            SearchActuator.SerialNumber,
            SearchActuator.PCBA.PCBAUid,
            SearchActuator.PCBA.ItemNumber,
            SearchActuator.PCBA.ManufacturerNumber,
            SearchActuator.PCBA.ProductionDateCode,
            SearchActuator.CreatedTimeStart,
            SearchActuator.CreatedTimeEnd,
            SearchActuator.PCBA.Software,
            SearchActuator.PCBA.ConfigNumber,
            SearchActuator.ArticleName,
            SearchActuator.ArticleNumber,
            SearchActuator.CommunicationProtocol);

        string base64String = Convert.ToBase64String(file);
        string dataUri = $"data:text/csv;base64,{base64String}";

        NavigationManager.NavigateTo(dataUri, forceLoad: true);
       // await JsRuntime.InvokeVoidAsync("jsSaveAsFile", "Actuators.txt", yikers);
    }
}