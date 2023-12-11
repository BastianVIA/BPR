using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace EndToEndTests;

public class GetPcbaForActuatorTests : PageTest
{
    private TestController _testController;

    public GetPcbaForActuatorTests()
    {
        _testController = new TestController();
    }

    [SetUp]
    public async Task Setup()
    {
        await Page.GotoAsync("localhost:5002/ActuatorSearch");
    }

    [TearDown]
    public void Teardown()
    {
        _testController.TestDone();
    }

    [Test]
    public async Task GetActuatorWithFilter_ShouldNotifyError_WhenNoSearchParametersGiven()
    {
        await Page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();
        var container = Page.Locator("#alert-container");
        await Expect(container).ToBeVisibleAsync();
        await Expect(container.Filter(new() { HasText = "Must specify at least one search parameter" }))
            .ToBeVisibleAsync();
    }

    [Test]
    public async Task GetActuatorWithFilter_ShouldShowEmptyTable_WhenNoSearchResultsFound()
    {
        await Page.Locator("input[name=\"WorkOrderNumber\"]").FillAsync("123456789");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();
        await Expect(Page.GetByText("No records to display.")).ToBeVisibleAsync();

    }

    [Test]
    public async Task GetActuatorWithFilter_ShouldReturnsPCBAUidForActuatorWithRightWOAndSerial_WhenMatchFound()
    {
        var expectedUid = "455711";
        var woNo = "825705";
        var serialNo = "9";
        await Page.Locator("input[name=\"WorkOrderNumber\"]").FillAsync(woNo);
        await Page.Locator("input[name=\"SerialNumber\"]").FillAsync(serialNo);
        await Page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();
        
        var rowSelector = ".rz-grid-table > tbody > tr:nth-child(1)";

        await Expect(Page.Locator(rowSelector)).ToContainTextAsync(woNo);
        await Expect(Page.Locator(rowSelector)).ToContainTextAsync(serialNo);
        await Expect(Page.Locator(rowSelector)).ToContainTextAsync(expectedUid);
    }


    [Test]
    public async Task GetActuatorWithFilter_ShouldOnlyReturnActuatorsWithExpectedWorkOrderNumber_WhenWorkOrderNumberMatches()
    {
        var expected = "31209343";
        await Page.Locator("input[name=\"WorkOrderNumber\"]").FillAsync(expected);
        await Page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();
        
        var colSelector = ".rz-grid-table > tbody > tr > td:nth-child(1)";
       
        var woNoCol = await Page.QuerySelectorAllAsync(colSelector);
        foreach (var row in woNoCol)
        {
            var value = await row.InnerTextAsync();
            Assert.AreEqual(expected, value);
        }
    }

    // [Test]
    // public async Task GetPCBAForActuator_ShouldNotifyError_WhenWONumberIsLargerThan8Digits()
    // {
    //     await Page.Locator("input[name=\"woNo\"]").FillAsync("123456789");
    //     await Page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();
    //     await Expect(Page.GetByText("WO Number must be 8 digits")).ToBeVisibleAsync();
    // }
    //
    // [Test]
    // public async Task GetPCBAForActuator_ShouldNotifyError_WhenWONumberIsLessThan8Digits()
    // {
    //     await Page.Locator("input[name=\"woNo\"]").FillAsync("1234567");
    //     await Page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();
    //     await Expect(Page.GetByText("WO Number must be 8 digits")).ToBeVisibleAsync();
    // }
    //
    // [Test]
    // public async Task GetPCBAForActuator_ShouldAcceptInput_WhenWONumberIs8Digits()
    // {
    //     await Page.Locator("input[name=\"woNo\"]").FillAsync("12345678");
    //     await Page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();
    //     await Expect(Page.GetByText("WO Number must be 8 digits")).ToBeHiddenAsync();
    // }
    //
    // [Test]
    // public async Task GetPCBAForActuator_ShouldNotifyError_WhenSerialNumberIsEmpty()
    // {
    //     await Page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();
    //     await Expect(Page.GetByText("Serial Number must be 1-4 digits")).ToBeVisibleAsync();
    // }
    //
    // [Test]
    // public async Task GetPCBAForActuator_ShouldNotifyError_WhenSerialNumberIsLargerThan4Digits()
    // {
    //     await Page.Locator("input[name=\"serialNo\"]").FillAsync("12345");
    //     await Page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();
    //     await Expect(Page.GetByText("Serial Number must be 1-4 digits")).ToBeVisibleAsync();
    // }
    //
    // [Test]
    // public async Task GetPCBAForActuator_ShouldAcceptInput_WhenSerialNumberIs4Digits()
    // {
    //     await Page.Locator("input[name=\"serialNo\"]").FillAsync("1234");
    //     await Page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();
    //     await Expect(Page.GetByText("Serial Number must be 1-4 digits")).ToBeHiddenAsync();
    // }
    //
    // [Test]
    // public async Task GetPCBAForActuator_ShouldAcceptInput_WhenSerialNumberIsLessThan4Digits()
    // {
    //     await Page.Locator("input[name=\"serialNo\"]").FillAsync("123");
    //     await Page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();
    //     await Expect(Page.GetByText("Serial Number must be 1-4 digits")).ToBeHiddenAsync();
    // }
    //
    // [Test]
    // public async Task GetPCBAForActuator_ShouldAcceptInput_WhenSerialNumberIs1Digit()
    // {
    //     await Page.Locator("input[name=\"serialNo\"]").FillAsync("1");
    //     await Page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();
    //     await Expect(Page.GetByText("Serial Number must be 1-4 digits")).ToBeHiddenAsync();
    // }
    //
    // [Test]
    // public async Task GetPCBAForActuator_ShouldNotifyError_WhenNotFound()
    // {
    //     await Page.Locator("input[name=\"woNo\"]").FillAsync("12345678");
    //     await Page.Locator("input[name=\"serialNo\"]").FillAsync("1");
    //     await Page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();
    //     var container = Page.Locator("#alert-container");
    //     await Expect(container).ToBeVisibleAsync();
    //     await Expect(container.Filter(new() {HasText = "Could not find actuator"})).ToBeVisibleAsync();
    // }
    //
    // [Test]
    // public async Task GetPCBAForActuator_ShouldReturnPCBAUid_WhenFound()
    // {
    //     await Page.Locator("input[name=\"woNo\"]").FillAsync("30912893");
    //     await Page.Locator("input[name=\"serialNo\"]").FillAsync("1");
    //
    //     await Page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();
    //     await Expect(Page.GetByText("PCBAUid")).ToContainTextAsync("656690");
    // }
}