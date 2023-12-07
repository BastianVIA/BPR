using System.Globalization;
using Frontend.Entities;
using Frontend.Model;
using Frontend.Service;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;

namespace Frontend.Pages;

public class TesterErrorsBase : ComponentBase
{
    public RadzenChart Chart { get; set; }
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
    public string SelectedTimePeriod { get; set; } = "Today";
    public List<TesterErrorsSet> DataSets { get; set; } = new();
    
    protected override async Task OnInitializedAsync()
    {
        SetTimePeriodOptions();
        await SetCellOptions();
    }
    

    private async Task SetCellOptions()
    {
        TesterOptions = await TesterErrorsModel.GetAllCellNames();
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
        return ((double)value).ToString(CultureInfo.CurrentCulture);
    }
    public string FormatDate(object value)
    {
        var numValue = Convert.ToDouble(value);
        var date = DateTime.FromOADate(numValue);
        //if (value is null) return string.Empty;
        var dateFormat = _dateFormatMap[_stringEnumMap[SelectedTimePeriod]];
        return date.ToString(dateFormat);
    }
    
    public string FormatDateToday(object value)
    {
        var date = Convert.ToDateTime(value);
       
        //if (value is null) return string.Empty;
        var dateFormat = _dateFormatMap[_stringEnumMap[SelectedTimePeriod]];
        return date.ToString(dateFormat);
    }
    
    public async Task OnUpdateGraph()
    {
        
        SelectedTesters ??= new List<string>();
        var selectedTime = _stringEnumMap[SelectedTimePeriod];
        DataSets = await TesterErrorsModel.GetTestErrorsForTesters(SelectedTesters, selectedTime);
        Mod();
    }

    public TimeSpan FormatXAxisStep()
    {
        switch (SelectedTimePeriod)
        {
            case "Today":
            case "Yesterday":
                return TimeSpan.FromMinutes(1);

            case "This_Week":
            case "Last_Full_Week":
                return TimeSpan.FromDays(1);

            case "This_Month":
            case "Last_Full_Month":
                return TimeSpan.FromDays(5);

            case "This_Year":
            case "Last_Full_Year":
                return TimeSpan.FromDays(30);

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Mod()
    {
        foreach (var error in DataSets[0].Errors)
        {
            error.ErrorCount = new Random().Next(20, 30);
        }

        DataSets[0].Errors[2].ErrorCount = 50;
    }

    public async Task OnApply()
    {
        
    }
}