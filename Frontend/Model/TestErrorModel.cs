using Frontend.Networking;
using Frontend.Util;

namespace Frontend.Model;

public class TestErrorModel : ITestErrorModel
{
    private INetwork _network;

    public TestErrorModel(INetwork network)
    {
        _network = network;
    }

    public async Task<TestErrorResponse> GetTestErrorsWithFilter(int? workOrderNumber, string? tester, int? bay,
        int? errorCode, DateTime startDate, DateTime? endDate, int timeIntervalBetweenRowsAsMinutes)
    {
        ValidateParams(workOrderNumber, tester, bay, errorCode, startDate, endDate, timeIntervalBetweenRowsAsMinutes);

        var networkResponse = await _network.GetTestErrorWithFilter(workOrderNumber, tester, bay, errorCode,
            startDate, endDate, timeIntervalBetweenRowsAsMinutes);

        List<GetTestErrorsWithFilterSingleLine> dataLinesList = new();

        foreach (var responseItem in networkResponse.DataLines)
        {
            List<GetTestErrorsWithFilterTestData> errorCodeList = new();

            foreach (var errorTestData in responseItem.ListOfErrors)
            {
                var errorCodeAndAmount = new GetTestErrorsWithFilterTestData()
                {
                    ErrorCode = errorTestData.ErrorCode,
                    AmountOfErrors = errorTestData.AmountOfErrors
                };

                errorCodeList.Add(errorCodeAndAmount);
            }

            var singleLine = new GetTestErrorsWithFilterSingleLine()
            {
                EndIntervalAsDate = responseItem.EndIntervalAsDate,
                TotalErrors = responseItem.TotalErrors,
                StartIntervalAsDate = responseItem.StartIntervalAsDate,
                TotalTests = responseItem.TotalTests,
                listOfErrors = errorCodeList
            };

            dataLinesList.Add(singleLine);
        }

        var testError = new TestErrorResponse
        {
            PossibleErrorCodes = networkResponse.PossibleErrorCodes.Select(code =>
                new GetTestErrorsWithFilterErrorCodeAndMessage
                {
                    ErrorCode = code.ErrorCode,
                    ErrorMessage = code.ErrorMessage
                }).ToList(),
            DataLines = dataLinesList
        };

        return testError;
    }

    private void ValidateParams(int? workOrderNumber, string? tester, int? bay,
        int? errorCode, DateTime startDate, DateTime? endDate, int timeIntervalBetweenRowsAsMinutes)
    {
        var endDateForValidation = endDate ?? DateTime.Now;

        if (timeIntervalBetweenRowsAsMinutes <= 0)
        {
            throw new ArgumentException("Time Interval Between Rows cannot be <= 0");
        }

        if (startDate >= endDateForValidation)
        {
            throw new ArgumentException("No time difference between start and end");
        }
        
        var timeDifference = endDateForValidation - startDate;
        var interval = TimeSpan.FromMinutes(timeIntervalBetweenRowsAsMinutes);
        if (interval > timeDifference)
        {
            throw new ArgumentException("Time Interval too large for Dates ");
        }

        if (timeDifference / interval > 1000)
        {
            throw new ArgumentException("Time Interval too small for the selected start date");
        }
        
    }
}