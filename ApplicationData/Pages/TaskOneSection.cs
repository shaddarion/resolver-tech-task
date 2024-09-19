using AutomationFramework.Controls.Interfaces;
using AutomationFramework.Controls;
using OpenQA.Selenium;

namespace ApplicationData.Pages
{
    public class TaskOneSection(IWebDriver driver) : BasePage(driver)
    {
        private ITextBox Email => new TextBox(_driver, By.Id("inputEmail"));
        private ITextBox Password => new TextBox(_driver, By.Id("inputPassword"));
        private IButton SignIn => new Button(_driver, By.XPath("//button[text() = 'Sign in']"));

        public TaskOneSection InputEmail(string value)
        {
            Email.SendKeys(value);
            return this;
        }

        public TaskOneSection InputPassword(string value)
        {
            Password.SendKeys(value);
            return this;
        }

        public TaskOneSection ClickSignIn()
        {
            SignIn.Click();
            return this;
        }

        public bool IsEmailInputPresent()
        {
            return Email.IsDisplayed;
        }

        public bool IsPasswordInputPresent()
        {
            return Password.IsDisplayed;
        }

        public bool IsSignInButtonPresent()
        {
            return SignIn.IsDisplayed;
        }
    }
}
