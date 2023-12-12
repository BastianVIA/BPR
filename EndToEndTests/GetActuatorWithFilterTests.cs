using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace EndToEndTests;

public class GetActuatorWithFilterTests : PageTest
{
    private TestController _testController;

    public GetActuatorWithFilterTests()
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
    public async Task GetActuatorWithFilter_ShouldReturnsSingleActuator_WhenWOAndSerialHasMatch()
    {
        var expectedUid = "455711";
        var woNo = "30686571";
        var serialNo = "33";
        await Page.Locator("input[name=\"WorkOrderNumber\"]").FillAsync(woNo);
        await Page.Locator("input[name=\"SerialNumber\"]").FillAsync(serialNo);
        await Page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();
        
        var rowSelector = ".rz-grid-table > tbody > tr:nth-child(1)";
        var rowsSelector = ".rz-grid-table > tbody > tr";
        
        await Page.GetByRole(AriaRole.Cell, new() { Name = "30686571" }).First.WaitForAsync(new() { State = WaitForSelectorState.Visible});
        var rows = await Page.Locator(rowsSelector).AllAsync();
        Assert.AreEqual(1, rows.Count);
        await Expect(Page.Locator(rowSelector)).ToContainTextAsync(woNo);
        await Expect(Page.Locator(rowSelector)).ToContainTextAsync(serialNo);
    }


    [Test]
    public async Task GetActuatorWithFilter_ShouldOnlyReturnListWithRightWO_WhenWOMatches()
    {
        var expected = "30686571";
        await Page.Locator("input[name=\"WorkOrderNumber\"]").FillAsync(expected);
        await Page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();
        
        var colSelector = ".rz-grid-table > tbody > tr > td:nth-child(1)";
        await Page.GetByRole(AriaRole.Cell, new() { Name = expected })
            .First.WaitForAsync(new() { State = WaitForSelectorState.Visible});
        var woNoCol = await Page.QuerySelectorAllAsync(colSelector);
        foreach (var row in woNoCol)
        {
            var value = await row.InnerTextAsync();
            Assert.AreEqual(expected, value);
        }
    }
}