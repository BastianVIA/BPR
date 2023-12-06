using System.ComponentModel.DataAnnotations;
using BuildingBlocks.Application;

namespace TestResult.Application.GetTestErrorsWithFilter;

public class GetTestErrorsWithFilterQuery : IQuery<GetTestErrorsWithFilterDto>
{
    [Required]
    public int TimeIntervalBetweenRowsAsMinutes { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    public int? WorkOrderNumber { get; set; }
    public string? Tester { get; set; }
    public int? Bay { get; set; }
    public int? ErrorCode { get; set; }

    
    public GetTestErrorsWithFilterQuery()
    {}
}