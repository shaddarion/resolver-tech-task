using AutomationFramework.Controls.Interfaces;
using AutomationFramework.Controls;
using OpenQA.Selenium;

namespace ApplicationData.Pages
{
    public class TaskThreeSection(IWebDriver driver) : BasePage(driver)
    {
        private IDropDown Options => new DropDown(_driver, By.Id("dropdownMenuButton"), By.ClassName("dropdown-item"));

        public string GetDropDownValue()
        {
            return Options.GetSelectedOption();
        }

        public void SelectDropDownValue(string value)
        {
            Options.SelectOption(value);
        }
    }
}
