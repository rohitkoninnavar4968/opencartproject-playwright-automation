using Microsoft.Playwright;

namespace Opencart_Automation_Project.Pages
{
    public class ProductSearchPage
    {
        private readonly IPage _page;

        public ProductSearchPage(IPage page)
        {
            _page = page;
        }

        
        private ILocator _txtSearch => _page.Locator("//input[@name='search']");
        private ILocator _btnSearch => _page.Locator("//button[@class='btn btn-light btn-lg']");
        private ILocator _productTiles => _page.Locator(".product-layout, .product-thumb, .product");
        private ILocator ProductLink(string productName) => _page.GetByText(productName, new() { Exact = true });
        private ILocator CategoryLink(string categoryName) => _page.GetByRole(AriaRole.Link, new() { Name = categoryName });

        public async Task SearchAsync(string query)
        {
            await _txtSearch.FillAsync(query);
            if (await _btnSearch.CountAsync() > 0)
                await _btnSearch.ClickAsync();
            else
                await _txtSearch.PressAsync("Enter");

            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }
        public async Task SearchProduct(string productName)  // Search method
        {
            await _txtSearch.FillAsync(productName);        // Enter product name
            await _btnSearch.ClickAsync();                  // Click search
        }

        public async Task NavigateToCategory(string categoryName)  // Navigate category
        {
            await _page.HoverAsync("a:has-text('Desktops')");      // Hover on menu
            await _page.ClickAsync($"a:has-text('{categoryName}')"); // Click subcategory
        }
        public async Task SelectCategoryAsync(string categoryName)
        {
            await CategoryLink(categoryName).ClickAsync();
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }

        public async Task OpenProductDetailsAsync(string productName)
        {
            await ProductLink(productName).ClickAsync();
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }

        // Helpers for assertions in tests
        public ILocator ProductLocator(string productName) => ProductLink(productName);

        public async Task<int> GetResultsCountAsync() => await _productTiles.CountAsync();
    }
}