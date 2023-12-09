using Frontend.Entities;
using Frontend.Util;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace Frontend.Components
{
    public class TestErrorTableBase : ComponentBase
    {
        [Parameter] public TestErrorResponse TestErrors { get; set; } = new();
        [Parameter] public string SelectedTimeIntervalBaseTable { get; set; }
        [Inject] public DialogService DialogService { get; set; }
        private List<string> Filters { get; set; } = new();
        protected SortOrder? TimeIntervalSortingOrder { get; set; }
        protected Dictionary<int, SortOrder?> ErrorColumnsSortingOrder = new();

        protected void AddColumnToSortingOrderDictionary(int errorCode)
        {
            ErrorColumnsSortingOrder.TryAdd(errorCode, null);
        }

        private void SetColumnSortingOrder(int errorCode, SortOrder? sortOrder)
        {
            TimeIntervalSortingOrder = null;
            ResetColumSortingOrders();
            ErrorColumnsSortingOrder[errorCode] = sortOrder;
        }

        private void ResetColumSortingOrders()
        {
            foreach (var kv in ErrorColumnsSortingOrder)
            {
                ErrorColumnsSortingOrder[kv.Key] = null;
            }
        }

        protected void OnSort(DataGridColumnSortEventArgs<GetTestErrorsWithFilterSingleLine> args)
        {
            var column = args.Column.ColumnPickerTitle;
            var direction = args.SortOrder;

            if (IsTimeInterval(column))
            {
                ResetColumSortingOrders();
                TestErrors.DataLines = TestErrors.DataLines.OrderBy(a => a.StartIntervalAsDate
                    ).ToList();

                if (direction == SortOrder.Descending)
                {
                    TestErrors.DataLines.Reverse();
                }
                TimeIntervalSortingOrder = direction;
                return;
            }

            if (!IsErrorColumn(column)) return;
            
            var errorCode = int.Parse(column.Replace("Error ", ""));
            TestErrors.DataLines = TestErrors.DataLines.OrderBy(a =>
                a.ListOfErrors.FirstOrDefault(b => b.ErrorCode == errorCode)?.AmountOfErrors ?? 0).ToList();
            
            SetColumnSortingOrder(errorCode, direction);
            if (direction == SortOrder.Descending)
            {
                TestErrors.DataLines.Reverse();
            }
            StateHasChanged();
        }

        protected async Task ShowErrorDetails(GetTestErrorsWithFilterSingleLine dataItem, int errorCode, string errorMessage)
        {
            var errorCount = dataItem.ListOfErrors.FirstOrDefault(test => test.ErrorCode == errorCode)?.AmountOfErrors ?? 0;
            if (errorCount == 0) return;
            
            var totalFailedTests = dataItem.TotalErrors;
            var percentage = totalFailedTests > 0 ? (errorCount / (float)totalFailedTests) * 100 : 0;
            
            await DialogService.OpenAsync<TestErrorChart>(
                "Error Percentage",
                new Dictionary<string, object>()
                {
                    { "Percentage", percentage },
                    { "ErrorCodeName", errorMessage }
                },
                new DialogOptions() { Width = "800px", Height = "600px" });
        }

        private bool IsTimeInterval(string columnName)
        {
            return columnName.Equals("Time Interval");
        }

        private bool IsErrorColumn(string columnName)
        {
            return !(columnName.Equals("Total Error Codes") || columnName.Equals("Total Tests") ||
                     columnName.Equals("Time Interval"));
        }

        protected bool ShouldShowColumn(string columnName)
        {
            return Filters.Contains(columnName);
        }

        protected int GetItemErrorCount(GetTestErrorsWithFilterSingleLine item, GetTestErrorsWithFilterErrorCodeAndMessage error)
        {
            var rowItem = item.ListOfErrors.FirstOrDefault(e => e.ErrorCode == error.ErrorCode);
            return rowItem?.AmountOfErrors ?? 0;
        }

        protected void UpdateFilters(List<string>? filters)
        {
            Filters = filters ?? new List<string>();
            StateHasChanged();
        }

        protected string FormatTimeInterval(DateTime start, DateTime end, string intervalBase)
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