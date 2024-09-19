using OpenQA.Selenium;
using System.Reflection;

namespace ApplicationData.Pages
{
    public class TaskPage(IWebDriver driver) : BasePage(driver)
    {
        public TaskOneSection TaskOne = new(driver);
        public TaskTwoSection TaskTwo = new(driver);
        public TaskThreeSection TaskThree = new(driver);
        public TaskFourSection TaskFour = new(driver);
        public TaskFiveSection TaskFive = new(driver);
        public TaskSixSection TaskSix = new(driver);

        public TaskPage Open()
        {
            var executionDirectoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _driver.Navigate().GoToUrl($"file://{executionDirectoryPath}\\AppFiles\\QE-index.html");

            return this;
        }        
    }
}
