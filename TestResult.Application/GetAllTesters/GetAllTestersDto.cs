namespace TestResult.Application.GetAllTesters;

public class GetAllTestersDto
{
    public List<string> AllTesters { get; }

    private GetAllTestersDto()
    {
    }

    private GetAllTestersDto(List<string> allTesters)
    {
        AllTesters = allTesters;
    }

    public static GetAllTestersDto From(List<string> allTesters)
    {
        return new GetAllTestersDto(allTesters);
    }
}