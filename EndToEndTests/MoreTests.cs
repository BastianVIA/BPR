namespace EndToEndTests;

public class MoreTests
{
    private TestController _testController;

    [OneTimeSetUp]
    public void Init()
    {
        _testController = TestController.Instance;
    }

    [SetUp]
    public void Setup()
    {
        var rem = TestController._testRemaining;
        Console.WriteLine();
    }

    [TearDown]
    public void TearDown()
    {
        _testController.TestDone();
    }

    [Test]
    public async Task Test1()
    {
        Assert.Pass();
    }
}