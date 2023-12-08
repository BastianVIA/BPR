using Frontend.Model;
using Frontend.Util;
using Microsoft.AspNetCore.Components;

namespace Frontend.Pages;

public class HomeBase : ComponentBase
{
    [Inject] private IStartUpAmountsModel startUpAmountsModel { get; set; }
    protected double SuccessRate { get; set; }
    protected double FirstAcceptanceRate { get; set; }
    protected int ActuatorAmount;
    
    protected class DataItem
    {
        public string Status { get; set; }
        public int Amount { get; set; }
    }

    protected DataItem[] Tests;
    protected DataItem[] FirstAcceptanceTests;

    protected override async Task OnInitializedAsync()
    {
        var startUpAmounts = await startUpAmountsModel.GetStartUpAmounts();
        ActuatorAmount = startUpAmounts.ActuatorAmount;
        SetupTotalTestChart(startUpAmounts);
        SetupFirstAcceptanceTestChart(startUpAmounts);
    }

    private void SetupFirstAcceptanceTestChart(StartUpAmounts startUpAmounts)
    {
        FirstAcceptanceTests = new[]
        {
            new DataItem()
            {
                Status = "Success",
                Amount = startUpAmounts.TestResultWithoutErrorAmount
            },
            new DataItem()
            {
                Status = "Failed",
                Amount = startUpAmounts.TestResultWithErrorAmount
            },
        };
        FirstAcceptanceRate = CalculateSuccessRate(FirstAcceptanceTests);
    }

    private void SetupTotalTestChart(StartUpAmounts startUpAmounts)
    {
        Tests = new[]
        {
            new DataItem()
            {
                Status = "Success",
                Amount = startUpAmounts.TestResultAmount
            },
            new DataItem()
            {
                Status = "Failed",
                Amount = startUpAmounts.TestErrorAmount
            },
        };
        SuccessRate = CalculateSuccessRate(Tests);
    }

    private double CalculateSuccessRate(DataItem[] dataItems)
    {
        return 100 - dataItems[1].Amount / (double)dataItems[0].Amount * 100;
    }
}