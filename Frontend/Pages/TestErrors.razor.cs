using Frontend.Exceptions;
using Frontend.Model;
using Frontend.Service.AlertService;
using Frontend.Util;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace Frontend.Pages;

public class TestErrorsBase : ComponentBase
{
    [Inject] public ITestErrorModel TestErrorModel { get; set; }
    [Inject] public IAlertService AlertService { get; set; }
    [Inject] public DialogService DialogService { get; set; }
    public TestErrorResponse TestErrors { get; set; } = new();
    protected SearchObject SearchTestError { get; set; } = new();
    public string SelectedTimeIntervalBase { get; set; } = "Hourly";
    public List<string> timeIntervalBases = new List<string> { "Hourly", "Daily", "Weekly", "Monthly", "Yearly" };

    protected class SearchObject
    {
        public int? WorkOrderNumber { get; set; }
        public string? Tester { get; set; }
        public int? Bay { get; set; }
        public int? ErrorCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public async Task OnChange()
    {
        await SearchTestErrors();
    }

    public async Task SearchTestErrors()
    {
        try
        {
            TestErrors = await TestErrorModel.GetTestErrorsWithFilter(
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
    }

    public int ConvertSelectionToMinutes()
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
}