using AutomationFramework.Controls.Interfaces;
using OpenQA.Selenium;

namespace AutomationFramework.Controls
{
    public class DropDown(IWebDriver driver, By dropdownButton, By dropdownMenu) : IDropDown
    {
        private readonly IButton DropDownButton = new Button(driver, dropdownButton);
        private readonly ILink DropDownMenuItem = new Link(driver, dropdownMenu);

        public void SelectOption(string option)
        {
            DropDownButton.Click();

            var designatedOption = DropDownMenuItem.GetAllControls<Link>().FirstOrDefault(x => x.Text == option);

            if (designatedOption != null)
            {
                designatedOption.Click();
            }
            else
            {
                throw new Exception($"Dropdown does not have option with value {option}.");
            }
        }

        public string GetSelectedOption()
        {
            return DropDownButton.Text;
        }
    }
}
