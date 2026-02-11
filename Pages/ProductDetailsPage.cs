using Microsoft.Playwright;

namespace Opencart_Automation_Project.Pages
{
    public class ProductDetailsPage
    {
        private readonly IPage _page;

        public ProductDetailsPage(IPage page)
        {
            _page = page;
        }

        private ILocator _title => _page.Locator("h1");
        private ILocator _price => _page.Locator(".price, .product-info .price, #content .price");
        private ILocator _addToCart => _page.GetByRole(AriaRole.Button, new() { Name = "Add to Cart" });

        public async Task<string> GetTitleAsync()
        {
            return (await _title.InnerTextAsync()).Trim();
        }

        public async Task<string?> GetPriceAsync()
        {
            if (await _price.CountAsync() == 0) return null;
            return (await _price.InnerTextAsync()).Trim();
        }

        public async Task ClickAddToCartAsync()
        {
            if (await _addToCart.CountAsync() > 0)
            {
                await _addToCart.ClickAsync();
                await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            }
        }
    }
}