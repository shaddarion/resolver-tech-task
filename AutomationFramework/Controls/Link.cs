using AutomationFramework.Controls.Interfaces;
using OpenQA.Selenium;

namespace AutomationFramework.Controls
{
    public class Link : Control, ILink
    {
        public Link(IWebDriver driver, By findElementBy) : base(driver, findElementBy)
        {
        }

        public Link(IWebDriver driver, IWebElement element) : base(driver, element)
        {
        }
    }
}
