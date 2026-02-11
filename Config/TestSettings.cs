using EaFramework.Driver;

namespace Opencart_Automation_Project.Config;

public class TestSettings
{
    public float? Timeout = PlaywrightDriverInitializer.DEFAULT_TIMEOUT;
    public DriverType DriverType { get; set; }
    public string ApplicationUrl { get; set; }
    public string[]? Args { get; set; }
    public bool? Headless { get; set; }
    public float? SlowMo { get; set; }
    public string ProductName { get; set; }
}

public enum DriverType
{
    Chrome,
    Firefox,
    Edge,
    Chromium,
    Opera
}