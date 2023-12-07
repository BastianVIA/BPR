using Frontend.Entities;
using Frontend.Networking;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Model
{
    public class TestErrorModel : ITestErrorModel
    {
        private INetwork _network;

        public TestErrorModel(INetwork network)
        {
            _network = network;
        }

        public async Task<TestError> GetTestErrorsWithFilter(int? workOrderNumber, string? tester, int? bay,
            int? errorCode, DateTime startDate, DateTime endDate, int timeIntervalBetweenRowsAsMinutes)
        {
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

            var testError = new TestError
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
    }
}