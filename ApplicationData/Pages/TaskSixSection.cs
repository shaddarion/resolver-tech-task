using ApplicationData.Components;
using OpenQA.Selenium;

namespace ApplicationData.Pages
{
    public class TaskSixSection(IWebDriver driver) : BasePage(driver)
    {
        public TableComponent Table = new(driver, By.XPath("//table[contains(@class, 'table')]"));
    }
}
