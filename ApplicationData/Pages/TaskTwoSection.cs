using ApplicationData.Components;
using OpenQA.Selenium;

namespace ApplicationData.Pages
{
    public class TaskTwoSection(IWebDriver driver) : BasePage(driver)
    {
        private By ListElement => By.XPath("//div[@id='test-2-div']//li[contains(@class, 'list-group-item')]");

        public List<ListElementComponent> TakeListElements()
        {
            return _driver.FindElements(ListElement).Select(x =>
            {
                var splittedTextArray = x.Text.Split(' ');

                if (splittedTextArray.Length >= 4)
                {
                    return new ListElementComponent
                    {
                        Text = string.Concat(splittedTextArray[0], " ", splittedTextArray[1], " ", splittedTextArray[2]),
                        BadgeText = splittedTextArray[3]
                    };
                }
                else
                {
                    return new ListElementComponent
                    {
                        Text = x.Text,
                        BadgeText = string.Empty
                    };
                }
            }).ToList();
        }
    }
}
