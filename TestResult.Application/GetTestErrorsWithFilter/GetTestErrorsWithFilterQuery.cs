using System.ComponentModel.DataAnnotations;
using BuildingBlocks.Application;

namespace TestResult.Application.GetTestErrorsWithFilter;

public class GetTestErrorsWithFilterQuery : IQuery<GetTestErrorsWithFilterDto>
{
    [Required] public int TimeIntervalBetweenRowsAsMinutes { get; set; }
    [Required] public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }  
    public int? WorkOrderNumber { get; set; }
    public string? Tester { get; set; }
    public int? Bay { get; set; }
    public int? ErrorCode { get; set; }


    public GetTestErrorsWithFilterQuery()
    {
    }

    public void Validate()
    {
        if (TimeIntervalBetweenRowsAsMinutes <= 0)
        {
            throw new ArgumentException("Time interval cannot be 0 or negative");
        }
    }

    public static GetTestErrorsWithFilterQuery Create(int timeIntervalBetweenRowsAsMinutes, DateTime startDat,
        DateTime? endDate, int? woNo= null, string? tester= null, int? bay= null, int? errorCode = null)
    {
        return new GetTestErrorsWithFilterQuery
        {
            TimeIntervalBetweenRowsAsMinutes = timeIntervalBetweenRowsAsMinutes,
            StartDate = startDat,
            EndDate = endDate,
            WorkOrderNumber = woNo,
            Tester = tester,
            Bay = bay,
            ErrorCode = errorCode
        };
    }
}