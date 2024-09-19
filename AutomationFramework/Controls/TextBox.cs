using AutomationFramework.Controls.Interfaces;
using OpenQA.Selenium;

namespace AutomationFramework.Controls
{
    public class TextBox(IWebDriver driver, By findElementBy) : Control(driver, findElementBy), ITextBox
    {
    }
}
