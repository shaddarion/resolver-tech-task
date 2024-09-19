using OpenQA.Selenium;

namespace ApplicationData.Pages
{
    public class BasePage(IWebDriver driver)
    {
        protected IWebDriver _driver = driver;
    }
}
