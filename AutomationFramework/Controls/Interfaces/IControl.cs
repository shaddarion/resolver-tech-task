using OpenQA.Selenium;

namespace AutomationFramework.Controls.Interfaces
{
    public interface IControl
    {
        IWebElement Element { get; }
        bool IsExist { get; }
        bool IsDisplayed { get; }
        string Text { get; }
        IEnumerable<T> GetAllControls<T>() where T : Control;
    }
}
