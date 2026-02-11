using Opencart_Automation_Project.Driver;
using Microsoft.Playwright;
using Opencart_Automation_Project.Config;
using Opencart_Automation_Project.Pages;
using NUnit.Framework;

namespace Opencart_Automation_Project.Tests;

[TestFixture]
public class ProductSearchTests
{
    private PlaywrightDriverInitializer _initializer;
    private TestSettings _testsettings;
    private PlaywrightDriver _playwrightDriver;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _initializer = new PlaywrightDriverInitializer();
    }

    [SetUp]
    public void SetUp()
    {
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

    [Test(Description = "Search should return results for known product")]
    public async Task Search_ReturnsResults_ForKnownProduct()
    {
        var page = await OpenHomeAsync();

        var search = new ProductSearchPage(page);
        await search.SearchAsync(_testsettings.ProductName);

        var count = await search.GetResultsCountAsync();
        Assert.That(count, Is.GreaterThan(0));

        var product = search.ProductLocator(_testsettings.ProductName);
        await Assertions.Expect(product).ToBeVisibleAsync();
    }

    [Test(Description = "Category navigation should show products in category")]
    public async Task CategoryNavigation_ShowsProduct()
    {
        var page = await OpenHomeAsync();

        var search = new ProductSearchPage(page);
        await search.SelectCategoryAsync("Phones & PDAs");

        var product = search.ProductLocator(_testsettings.ProductName);
        await Assertions.Expect(product).ToBeVisibleAsync();
    }

    [Test(Description = "Search and open product details should show title and price")]
    public async Task Search_OpenProductDetails_VerifyTitleAndPrice()
    {
        var page = await OpenHomeAsync();

        var search = new ProductSearchPage(page);
        var details = new ProductDetailsPage(page);

        await search.SearchAsync(_testsettings.ProductName);
        await search.OpenProductDetailsAsync(_testsettings.ProductName);

        var title = await details.GetTitleAsync();
        Assert.That(title, Does.Contain("iPhone").IgnoreCase);

        var price = await details.GetPriceAsync();
        Assert.That(price, Is.Not.Null.And.Not.Empty);
    }

    [TearDown]
    public async Task TearDown()
    {
        _playwrightDriver.Dispose();
    }

    
}