using Frontend.Entities;
using Frontend.Util;
using Microsoft.AspNetCore.Components;

namespace Frontend.Components
{
    public class TestErrorTableBase : ComponentBase
    {
        [Parameter] public TestErrorResponse TestErrors { get; set; } = new();
        [Parameter] public string SelectedTimeIntervalBaseTable { get; set; }
        public List<string> Filters { get; set; } = new List<string>();
        public List<string> ErrorCodes { get; set; }
        public Dictionary<int, string> ErrorCodeMessages { get; set; } = new Dictionary<int, string>();

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            InitializeErrorCodes();
            InitializeErrorMessages();
        }

        private void InitializeErrorCodes()
        {
            ErrorCodes = TestErrors.PossibleErrorCodes
                .Select(ec => ec.ErrorCode.ToString())
                .ToList();
        }

        private void InitializeErrorMessages()
        {
            ErrorCodeMessages = TestErrors.PossibleErrorCodes
                .ToDictionary(ec => ec.ErrorCode, ec => ec.ErrorMessage);
        }

        public bool ShouldShowColumn(string columnName)
        {
            return Filters.Contains(columnName);
        }

        public void UpdateFilters(List<string> filters)
        {
            Filters = filters ?? new List<string>();
            StateHasChanged();
        }

        public List<Dictionary<string, object>> PivotDataLines(List<GetTestErrorsWithFilterSingleLine> dataLines,
            string intervalBase)
        {
            var pivotedList = new List<Dictionary<string, object>>();
            foreach (var line in dataLines)
            {
                var timeInterval = FormatTimeInterval(line.StartIntervalAsDate, line.EndIntervalAsDate, intervalBase);

                var pivotRow = new Dictionary<string, object>
                {
                    { "TimeInterval", timeInterval },
                    { "TotalErrors", line.TotalErrors },
                    { "TotalTests", line.TotalTests }
                };

                foreach (var code in ErrorCodes)
                {
                    var errorCount = line.listOfErrors.FirstOrDefault(e => e.ErrorCode.ToString() == code)
                        ?.AmountOfErrors ?? 0;
                    pivotRow.Add($"ErrorCode{code}", errorCount);

                    if (ErrorCodeMessages.TryGetValue(int.Parse(code), out var errorMessage))
                    {
                        pivotRow.Add($"ErrorMessage{code}", errorMessage);
                    }
                    else
                    {
                        pivotRow.Add($"ErrorMessage{code}", "No message available");
                    }
                }

                pivotedList.Add(pivotRow);
            }

            return pivotedList;
        }
        private string FormatTimeInterval(DateTime start, DateTime end, string intervalBase)
        {
            switch (intervalBase)
            {
                case "Daily":
                    return $"{start:yyyy-MM-dd}";
                case "Weekly":
                    var weekStart = start.AddDays(-(int)start.DayOfWeek);
                    var weekEnd = weekStart.AddDays(6);
                    return $"{weekStart:yyyy-MM-dd} - {weekEnd:yyyy-MM-dd}";
                case "Monthly":
                    return $"{start:yyyy-MM}";
                case "Yearly":
                    return $"{start:yyyy}";
                default:
                    return $"{start:HH:mm} - {end:HH:mm}";
            }
        }
    }
}