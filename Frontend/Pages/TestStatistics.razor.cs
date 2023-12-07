using Frontend.Entities;
using Frontend.Exceptions;
using Frontend.Model;
using Frontend.Service.AlertService;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace Frontend.Pages;

public class TestStatisticsBase : ComponentBase
{
    [Inject] public ITestErrorModel TestErrorModel { get; set; }
    [Inject] public IAlertService AlertService { get; set; }
    [Inject] public DialogService DialogService { get; set; }

    public TestError TestErrors { get; set; } = new TestError();
    protected SearchObject SearchTestError { get; set; } = new SearchObject();
    public string SelectedTimeIntervalBase { get; set; } = "Minutes";
    public List<string> timeIntervalBases = new List<string> { "Minutes", "Daily", "Weekly", "Monthly", "Yearly" };
 

    protected class SearchObject
    {
        public int? WorkOrderNumber { get; set; }
        public string? Tester { get; set; }
        public int? Bay { get; set; }
        public int? ErrorCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TimeIntervalBetweenRowsAsMinutes { get; set; }
        
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
                ConvertingFromMinutes(SearchTestError.TimeIntervalBetweenRowsAsMinutes));
        }
        catch (NetworkException e)
        {
            AlertService.FireEvent(AlertStyle.Danger, e.Message);
        }
    }


    public int ConvertingFromMinutes(int timeIntervalBetweenRowsAsMinutes)
    {
        switch (SelectedTimeIntervalBase)
        {
            case "Daily":
                return timeIntervalBetweenRowsAsMinutes * 24 * 60;
            case "Weekly":
                return timeIntervalBetweenRowsAsMinutes * 24 * 60 * 7;
            case "Monthly":
                return timeIntervalBetweenRowsAsMinutes * 24 * 60 * 30;
            case "Yearly":
                return timeIntervalBetweenRowsAsMinutes * 24 * 60 * 365;
            default:
                return timeIntervalBetweenRowsAsMinutes; 
        }
    }
}