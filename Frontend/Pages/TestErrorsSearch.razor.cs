using Frontend.Exceptions;
using Frontend.Model;
using Frontend.Service.AlertService;
using Frontend.Util;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace Frontend.Pages;

public class TestErrorsSearchBase : ComponentBase
{
    [Inject] public ITestErrorsSearchModel TestErrorsSearchModel { get; set; }
    [Inject] public IAlertService AlertService { get; set; }
    protected TestErrorResponse TestErrors { get; set; } = new();
    protected SearchObject SearchTestError { get; set; } = new();
    protected string SelectedTimeIntervalBase { get; set; } = "Hourly";
    protected List<string> timeIntervalBases = new() { "Hourly", "Daily", "Weekly", "Monthly", "Yearly" };
    
    // Base constructor used by Blazor
    public TestErrorsSearchBase()
    {
        
    }
    
    // Overloaded constructor, to inject model and alert service, needed by tests
    public TestErrorsSearchBase(ITestErrorsSearchModel testErrorsSearchModel, IAlertService alertService)
    {
        TestErrorsSearchModel = testErrorsSearchModel;
        AlertService = alertService;
    }

    protected async Task OnChange()
    {
        await SearchTestErrors();
    }

    protected async Task SearchTestErrors()
    {
        try
        {
            TestErrors = await TestErrorsSearchModel.GetTestErrorsWithFilter(
                SearchTestError.WorkOrderNumber,
                SearchTestError.Tester,
                SearchTestError.Bay,
                SearchTestError.ErrorCode,
                SearchTestError.StartDate,
                SearchTestError.EndDate,
                ConvertSelectionToMinutes());
        }
        catch (NetworkException e)
        {
            AlertService.FireEvent(AlertStyle.Danger, e.Message);
        }
        catch (ArgumentException e)
        {
            AlertService.FireEvent(AlertStyle.Danger, e.Message);
        }
    }

    private int ConvertSelectionToMinutes()
    {
        switch (SelectedTimeIntervalBase)
        {
            case "Hourly":
                return 60;
            case "Daily":
                return 24 * 60;
            case "Weekly":
                return 24 * 60 * 7;
            case "Monthly":
                return 24 * 60 * 30;
            case "Yearly":
                return 24 * 60 * 365;
            default:
                throw new InvalidDataException();
        }
    }
    
    protected class SearchObject
    {
        public int? WorkOrderNumber { get; set; }
        public string? Tester { get; set; }
        public int? Bay { get; set; }
        public int? ErrorCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}