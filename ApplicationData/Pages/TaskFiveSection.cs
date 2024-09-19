using AutomationFramework.Controls.Interfaces;
using AutomationFramework.Controls;
using OpenQA.Selenium;

namespace ApplicationData.Pages
{
    public class TaskFiveSection(IWebDriver driver) : BasePage(driver)
    {
        private IButton Button => new Button(_driver, By.Id("test5-button"));
        private ILabel Message => new Label(_driver, By.Id("test5-alert"));

        public void ClickButton()
        {
            Button.Click();
        }

        public bool IsSuccessMessageDisplayed()
        {
            return Message.IsDisplayed;
        }

        public string GetMessageText()
        {
            return Message.Text;
        }

        public bool IsButtonDisplayed()
        {
            return Button.IsDisplayed;
        }

        public bool IsButtonEnabled()
        {
            return Button.Element.Enabled;
        }
    }
}
