using OpenQA.Selenium;

namespace ApplicationData.Pages
{
    public class PageFacade(IWebDriver driver)
    {
        public readonly Lazy<TaskPage> TaskPage = new(() => new TaskPage(driver));
    }
}
