using Frontend.Entities;
using Frontend.Exceptions;
using Frontend.Model;
using Frontend.Service;
using Frontend.Service.AlertService;
using Frontend.Services;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace Frontend.Pages;

public class ActuatorSearchBase : ComponentBase
{
    protected class SearchObject
    {
        public int? WorkOrderNumber { get; set; }
        public int? SerialNumber { get; set; }
        public string? ArticleNumber { get; set; }
        public string? ArticleName { get; set; }
        public string? CommunicationProtocol { get; set; }
        public DateTime? CreatedTimeStart { get; set; }
        public DateTime? CreatedTimeEnd { get; set; }
        public string? PCBAUid { get; set; }
        public string? PCBAItemNumber { get; set; }
        public int? PCBAManufacturerNumber { get; set; }
        public int? PCBAProductionDateCode { get; set; }
        public string? PCBASoftware { get; set; }
        public string? PCBAConfigNumber { get; set; }
    }

    [Inject] public IActuatorSearchModel SearchModel { get; set; }

    [Inject] public IActuatorSearchCsvModel SearchCsvModel { get; set; }
    
    [Inject] public IActuatorDetailsModel DetailsModel { get; set; }

    [Inject] public IAlertService AlertService { get; set; }

    [Inject] public DialogService DialogService { get; set; }
    
    [Inject] public NavigationManager NavigationManager { get; set; }

    // Radzen needs a class to specify the data object
    protected SearchObject SearchActuator { get; } = new();

    public List<Actuator> actuators = new();

    private List<CsvProperties> _selectedFilters = new();

    // Blazor page needs an empty constructor
    public ActuatorSearchBase()
    {
    }

    // Overloaded constructor for unit testing
    public ActuatorSearchBase(IActuatorSearchModel model)
    {
        SearchModel = model;
    }

    protected async Task SearchActuators()
    {
        try
        {
            actuators = await SearchModel.GetActuatorWithFilter(
                SearchActuator.WorkOrderNumber,
                SearchActuator.SerialNumber,
                SearchActuator.PCBAUid,
                SearchActuator.PCBAItemNumber,
                SearchActuator.PCBAManufacturerNumber,
                SearchActuator.PCBAProductionDateCode,
                SearchActuator.CreatedTimeStart,
                SearchActuator.CreatedTimeEnd,
                SearchActuator.PCBASoftware,
                SearchActuator.PCBAConfigNumber,
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

    protected void OnColumnsUpdated(List<CsvProperties> filters)
    {
        _selectedFilters = filters;
    }

    protected async Task ShowActuatorDetails(Actuator actuator)
    {
        await DialogService.OpenAsync<InformationContainer>($"Details",
            new Dictionary<string, object>() { { "Actuator", actuator } },
            new DialogOptions() { Width = "700px", Height = "530px", Resizable = true, Draggable = true });
        await SearchActuators();
    }

    public async Task DownloadActuators()
    {
        var file = await SearchCsvModel.GetActuatorWithFilter(_selectedFilters,
            SearchActuator.WorkOrderNumber,
            SearchActuator.SerialNumber,
            SearchActuator.PCBAUid,
            SearchActuator.PCBAItemNumber,
            SearchActuator.PCBAManufacturerNumber,
            SearchActuator.PCBAProductionDateCode,
            SearchActuator.CreatedTimeStart,
            SearchActuator.CreatedTimeEnd,
            SearchActuator.PCBASoftware,
            SearchActuator.PCBAConfigNumber,
            SearchActuator.ArticleName,
            SearchActuator.ArticleNumber,
            SearchActuator.CommunicationProtocol);

        string base64String = Convert.ToBase64String(file);
        string dataUri = $"data:text/csv;base64,{base64String}";

        NavigationManager.NavigateTo(dataUri, forceLoad: true);
    }
}