namespace AutomationFramework.Controls.Interfaces
{
    public interface ITextBox : IControl
    {
        void SendKeys(string value, bool isNeedClearBefore = true);
    }
}
