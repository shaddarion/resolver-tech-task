using AutomationFramework.Controls.Interfaces;
using OpenQA.Selenium;

namespace AutomationFramework.Controls
{
    public class Button(IWebDriver driver, By findElementBy) : Control(driver, findElementBy), IButton
    {
    }
}
