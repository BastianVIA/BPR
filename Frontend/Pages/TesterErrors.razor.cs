using System.Globalization;
using Frontend.Entities;
using Frontend.Model;
using Frontend.Service;
using Microsoft.AspNetCore.Components;

namespace Frontend.Pages;

public class TesterErrorsBase : ComponentBase
{
    protected class SearchObject
    {
        public List<string> Testers { get; set; }
    }
    
    [Inject] private ITesterErrorsModel TesterErrorsModel { get; set; }

    private Dictionary<TesterTimePeriodEnum, string> _dateFormatMap = new()
    {
        {TesterTimePeriodEnum.This_Year, "dd MMM yy"},
        {TesterTimePeriodEnum.Last_Full_Year, "dd MMM yy"},
        { TesterTimePeriodEnum.This_Month, "dd MMM"},
        { TesterTimePeriodEnum.Last_Full_Month, "dd MMM"},
        { TesterTimePeriodEnum.This_Week, "dd MMM" },
        { TesterTimePeriodEnum.Last_Full_Week, "dd MMM"},
        { TesterTimePeriodEnum.Today, "HH:mm"},
        { TesterTimePeriodEnum.Yesterday, "HH:mm"}
    };

    private Dictionary<string, TesterTimePeriodEnum> _stringEnumMap = new()
    {
        { "This Year",TesterTimePeriodEnum.This_Year},
        {"Last Full Year", TesterTimePeriodEnum.Last_Full_Year},
        {"This Month", TesterTimePeriodEnum.This_Month},
        {"Last Full Month", TesterTimePeriodEnum.Last_Full_Month},
        {"This Week", TesterTimePeriodEnum.This_Week },
        {"Last Full Week", TesterTimePeriodEnum.Last_Full_Week},
        {"Today", TesterTimePeriodEnum.Today },
        {"Yesterday", TesterTimePeriodEnum.Yesterday}
    };

    public List<string> TesterOptions { get; set; } = new();
    public List<string>? SelectedTesters { get; set; }
    public List<string> TimePeriodOptions { get; set; } = new();
    public string SelectedTimePeriod { get; set; }
    public List<TesterErrorsSet> DataSets { get; set; } = new();

    protected async override Task OnInitializedAsync()
    {
        SetTimePeriodOptions();
        await SetCellOptions();
        
    }

    public async Task SetCellOptions()
    {
        TesterOptions = new List<string>()
        {
            "LASPRD-C22873TT, LA36 Pre-Tester ManualTwo-Cell, 2023-0xx, LINTest 11.0.22.0",
        };
    }
    
    private void SetTimePeriodOptions()
    {
        foreach (var kv in _dateFormatMap)
        {
            TimePeriodOptions.Add(kv.Key.ToString().Replace("_", " "));
        }
    }

    public string FormatYAxis(object value)
    {
        return ((double)value).ToString(CultureInfo.InvariantCulture);
    }

    private DateTime currentMonth;
    private DateTime? lastMonth;

    public string FormatDate(object value)
    {
        var dateFormat = _dateFormatMap[_stringEnumMap[SelectedTimePeriod]];
        lastMonth = currentMonth;
        return Convert.ToDateTime(value).ToString(dateFormat);
    }
    
    public async Task OnChange()
    {
        DataSets.Clear();
        DataSets = await TesterErrorsModel.GetTestErrorsForTesters(SelectedTesters ?? new List<string>(), _stringEnumMap[SelectedTimePeriod]);
        Console.WriteLine();
    }
}