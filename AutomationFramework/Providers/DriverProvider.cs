using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Reflection;

namespace AutomationFramework.Providers
{
    public static class DriverProvider
    {
        public static IWebDriver InitializeChromeDriver()
        {
            IWebDriver driver;
            var chromeOptions = new ChromeOptions();

            if (ApplicationSettings.NeedMaximizeWindow)
            {
                chromeOptions.AddArguments("--start-maximized");
            }
            else
            {
                chromeOptions.AddArguments("--window-size=2560,1440");
            }

            chromeOptions.AddArgument("no-sandbox");
            chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");

            if (ApplicationSettings.Headless)
            {
                chromeOptions.AddArguments("--headless=new");
            }

            var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            driver = new ChromeDriver(assemblyPath, chromeOptions, TimeSpan.FromSeconds(ApplicationSettings.DefaulCommandTimeout));
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(ApplicationSettings.PageLoadTimeout);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(ApplicationSettings.ImplicitWaitTimeout);

            return driver;
        }
    }
}