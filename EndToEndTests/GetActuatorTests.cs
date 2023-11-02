using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace EndToEndTests;

public class GetActuatorTests : PageTest
{
    private TestController? _testController;
    [OneTimeSetUp]
    public void Init()
    {
        _testController = TestController.Instance;
    }

    [SetUp]
    public async Task Setup()
    {
        await Page.GotoAsync("localhost:5002/PCBAInfo");
    }

    [TearDown]
    public void Teardown()
    {
        _testController!.TestDone();
    }

    [Test]
    public async Task GetActuatorDetails_ShouldNotifyError_WhenWONumberIsEmpty()
    {
        await Page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();
        await Expect(Page.GetByText("WO Number must be 8 digits")).ToBeVisibleAsync();
    }

    [Test]
    public async Task GetActuatorDetails_ShouldNotifyError_WhenWONumberIsLargerThan8Digits()
    {
        await Page.Locator("input[name=\"woNo\"]").FillAsync("123456789");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();
        await Expect(Page.GetByText("WO Number must be 8 digits")).ToBeVisibleAsync();
    }
    
    [Test]
    public async Task GetActuatorDetails_ShouldNotifyError_WhenWONumberIsLessThan8Digits()
    {
        await Page.Locator("input[name=\"woNo\"]").FillAsync("1234567");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();
        await Expect(Page.GetByText("WO Number must be 8 digits")).ToBeVisibleAsync();
    }
    
    [Test]
    public async Task GetActuatorDetails_ShouldAcceptInput_WhenWONumberIs8Digits()
    {
        await Page.Locator("input[name=\"woNo\"]").FillAsync("12345678");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();
        await Expect(Page.GetByText("WO Number must be 8 digits")).ToBeHiddenAsync();
    }

    [Test]
    public async Task GetActuatorDetails_ShouldNotifyError_WhenSerialNumberIsEmpty()
    {
        await Page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();
        await Expect(Page.GetByText("Serial Number must be 1-4 digits")).ToBeVisibleAsync();
    }
    
    [Test]
    public async Task GetActuatorDetails_ShouldNotifyError_WhenSerialNumberIsLargerThan4Digits()
    {
        await Page.Locator("input[name=\"serialNo\"]").FillAsync("12345");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();
        await Expect(Page.GetByText("Serial Number must be 1-4 digits")).ToBeVisibleAsync();
    }
    
    [Test]
    public async Task GetActuatorDetails_ShouldNotifyError_WhenSerialNumberIsLessThan4Digits()
    {
        await Page.Locator("input[name=\"serialNo\"]").FillAsync("123");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();
        await Expect(Page.GetByText("Serial Number must be 1-4 digits")).ToBeHiddenAsync();
    }
    
    [Test]
    public async Task GetActuatorDetails_ShouldNotifyError_WhenSerialNumberIs4Digits()
    {
        await Page.Locator("input[name=\"serialNo\"]").FillAsync("1234");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();
        await Expect(Page.GetByText("Serial Number must be 1-4 digits")).ToBeHiddenAsync();
    }
    
    [Test]
    public async Task GetActuatorDetails_ShouldAcceptInput_WhenSerialNumberIs1Digit()
    {
        await Page.Locator("input[name=\"woNo\"]").FillAsync("1");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();
        await Expect(Page.GetByText("Serial Number must be 1-4 digits")).ToBeHiddenAsync();
    }
    

    [Test]
    public async Task GetActuatorInfo_ShouldNotifyError_WhenNotFound()
    {
        await Page.Locator("input[name=\"woNo\"]").FillAsync("12345678");
        await Page.Locator("input[name=\"serialNo\"]").FillAsync("1");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();
        var container = Page.Locator("#alert-container");
        await Expect(container).ToBeVisibleAsync();
        await Expect(container.Filter(new() {HasText = "Could not find actuator"})).ToBeVisibleAsync();
    }
    
    [Test]
    public async Task GetActuatorInfo_ShouldReturnPCBAUid_WhenFound()
    {
        await Page.Locator("input[name=\"woNo\"]").FillAsync("30912893");
        await Page.Locator("input[name=\"serialNo\"]").FillAsync("1");

        await Page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();
        await Expect(Page.GetByText("PCBAUid")).ToContainTextAsync("656690");
    }
}