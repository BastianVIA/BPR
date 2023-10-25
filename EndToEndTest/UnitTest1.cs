using Microsoft.Playwright;

namespace EndToEndTest;

public class Tests
{
    [Test]
    public async Task Test1()
    {
        using var playwright = await Playwright.CreateAsync();

        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false
        });

        var page = await browser.NewPageAsync();
        await page.GotoAsync("localhost:5000");

    }
}