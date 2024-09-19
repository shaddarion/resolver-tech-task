using AutomationFramework.Controls.Interfaces;
using AutomationFramework.Controls;
using OpenQA.Selenium;

namespace ApplicationData.Pages
{
    public class TaskFourSection(IWebDriver driver) : BasePage(driver)
    {
        private IButton LeftButton => new Button(_driver, By.XPath("//div[@id = 'test-4-div']//button[contains(@class, 'btn-primary')]"));
        private IButton RightButton => new Button(_driver, By.XPath("//div[@id = 'test-4-div']//button[contains(@class, 'btn-secondary')]"));

        public bool IsLeftButtonEnabled()
        {
            return LeftButton.Element.Enabled;
        }

        public bool IsRightButtonEnabled()
        {
            return RightButton.Element.Enabled;
        }
    }
}
