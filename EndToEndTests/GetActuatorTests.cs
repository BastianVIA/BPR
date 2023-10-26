using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace EndToEndTests;

public class GetActuatorTests : PageTest
{
    private TestController _testController;
    [OneTimeSetUp]
    public void Init()
    {
        _testController = TestController.Instance;
    }

    [SetUp]
    public async Task Setup()
    {
        var rem = TestController._testRemaining;
        await Page.GotoAsync("localhost:5002/PCBAInfo");
    }

    [TearDown]
    public void Teardown()
    {
        _testController.TestDone();
    }

    [Test]
    public async Task Test1()
    {
        await Page.Locator("input[name=\"woNo\"]").FillAsync("555555");
    }
    
    [Test]
    public async Task Test2()
    {
        await Page.Locator("input[name=\"woNo\"]").FillAsync("30912893");
        await Page.Locator("input[name=\"serialNo\"]").FillAsync("1");

        await Page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();
        await Expect(Page.GetByText("PCBAUid")).ToContainTextAsync("656690");
    }
}