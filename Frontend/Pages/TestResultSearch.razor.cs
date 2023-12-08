using Frontend.Components;
using Frontend.Entities;
using Frontend.Model;
using Frontend.Service.AlertService;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace Frontend.Pages;

public class TestResultSearchBase : ComponentBase
{
    protected class SearchObject
    {
        public int? WorkOrderNumber { get; set; }
        public int? SerialNumber { get; set; }
        public string? Tester { get; set; }
        public int? Bay { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
    [Inject] public IAlertService AlertService { get; set; }
    [Inject] public ITestResultSearchModel TestResultSearchModel { get; set; }
    [Inject] public DialogService DialogService { get; set; }
    protected SearchObject SearchTestResult { get; } = new();
    protected List<TestResult> TestResults { get; set; } = new();

    public TestResultSearchBase()
    {
        
    }

    public TestResultSearchBase(ITestResultSearchModel testResultSearchModel)
    {
        TestResultSearchModel = testResultSearchModel;
    }
    protected async Task Search()
    {
        try
        {
            TestResults = await TestResultSearchModel.GetTestResultsWithFilter(SearchTestResult.WorkOrderNumber, SearchTestResult.SerialNumber, SearchTestResult.Tester, SearchTestResult.Bay, SearchTestResult.StartDate, SearchTestResult.EndDate);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            AlertService.FireEvent(AlertStyle.Danger, e.Message);
        }
    }
    protected async Task ShowTestResultDetails(TestResult testResult)
    {
        await DialogService.OpenAsync<TestResultDetails>($"Test Details",
            new Dictionary<string, object> { { "TestResult", testResult } },
            new DialogOptions() { Width = "90%", Height = "80%"});
    }
}