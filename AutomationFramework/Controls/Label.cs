using AutomationFramework.Controls.Interfaces;
using OpenQA.Selenium;

namespace AutomationFramework.Controls
{
    public class Label(IWebDriver driver, By findElementBy) : Control(driver, findElementBy), ILabel
    {
    }
}
