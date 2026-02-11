//using EaApplicationTest.Pages;
using EaFramework.Config;
using EaFramework.Driver;
using Microsoft.Playwright;
using Opencart_Automation_Project.Pages;
using Xunit;

namespace Opencart_Automation_Project.Tests;

public class ProductSearchTests : IClassFixture<PlaywrightDriverInitializer>, IDisposable
{
    private readonly PlaywrightDriverInitializer _initializer;
    private readonly TestSettings _testsettings;
    private readonly PlaywrightDriver _playwrightDriver;

    public ProductSearchTests(PlaywrightDriverInitializer initializer)
    {
        _initializer = initializer;
        _testsettings = ConfigReader.ReadConfig();
        _playwrightDriver = new PlaywrightDriver(_testsettings, _initializer);
    }

    private async Task<IPage> OpenHomeAsync()
    {
        var page = await _playwrightDriver.Page;
        await page.GotoAsync(_testsettings.ApplicationUrl);
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        return page;
    }

    [Fact(DisplayName = "Search should return results for known product")]
    public async Task Search_ReturnsResults_ForKnownProduct()
    {
        var page = await OpenHomeAsync();

        var search = new ProductSearchPage(page);
        await search.SearchAsync(_testsettings.ProductName);

        var count = await search.GetResultsCountAsync();
        Assert.That(count > 0);

        var product = search.ProductLocator(_testsettings.ProductName);
        await Assertions.Expect(product).ToBeVisibleAsync();
    }

    [Fact(DisplayName = "Category navigation should show products in category")]
    public async Task CategoryNavigation_ShowsProduct()
    {
        var page = await OpenHomeAsync();

        var search = new ProductSearchPage(page);
        await search.SelectCategoryAsync("Phones & PDAs");

        var product = search.ProductLocator(_testsettings.ProductName);
        await Assertions.Expect(product).ToBeVisibleAsync();
    }

    [Fact(DisplayName = "Search and open product details should show title and price")]
    public async Task Search_OpenProductDetails_VerifyTitleAndPrice()
    {
        var page = await OpenHomeAsync();

        var search = new ProductSearchPage(page);
        var details = new ProductDetailsPage(page);

        await search.SearchAsync(_testsettings.ProductName);
        await search.OpenProductDetailsAsync(_testsettings.ProductName);

        var title = await details.GetTitleAsync();
        Assert.That(title.Contains("iPhone", StringComparison.OrdinalIgnoreCase));

        var price = await details.GetPriceAsync();
        Assert.That(!string.IsNullOrWhiteSpace(price));
    }

    public void Dispose()
    {
        _playwrightDriver.Dispose();
    }
}