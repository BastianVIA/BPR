using BuildingBlocks.Application;
using TestResult.Domain.Repositories;

namespace TestResult.Application.GetAllTesters;

public class GetAllTestersQueryHandler : IQueryHandler<GetAllTestersQuery,GetAllTestersDto>
{
    private readonly ITestResultRepository _testResultRepository;

    public GetAllTestersQueryHandler( ITestResultRepository testResultRepository)
    {
        _testResultRepository = testResultRepository;
    }

    public async Task<GetAllTestersDto> Handle(GetAllTestersQuery request, CancellationToken cancellationToken)
    {
        var allTesters = await _testResultRepository.GetAllTesters();
        return GetAllTestersDto.From(allTesters);
    }
}