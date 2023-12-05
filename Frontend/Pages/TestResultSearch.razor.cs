using Frontend.Entities;
using Frontend.Model;
using Frontend.Service.AlertService;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace Frontend.Pages;

public class TestResultSearchBase : ComponentBase
{
    [Inject] public IAlertService AlertService { get; set; }
    [Inject] public ITestResultSearchModel TestResultSearchModel { get; set; }
    protected TestResult SearchTestResult { get; } = new();
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
            TestResults = await TestResultSearchModel.GetTestResultsWithFilter(1);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            AlertService.FireEvent(AlertStyle.Danger, "Error Test Result");
        }
    }
}