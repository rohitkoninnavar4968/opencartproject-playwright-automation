using Microsoft.Playwright;
using Opencart_Automation_Project.Config;

namespace Opencart_Automation_Project.Driver;

public interface IPlaywrightDriverInitializer
{
    Task<IBrowser> GetChromeDriverAsync(TestSettings testSettings);
    Task<IBrowser> GetChromiumDriverAsync(TestSettings testSettings);
    Task<IBrowser> GetFirefoxDriverAsync(TestSettings testSettings);
    Task<IBrowser> GetWebKitDriverAsync(TestSettings testSettings);
}