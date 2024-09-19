using AutomationFramework.Helpers;
using AutomationFramework.Providers;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using Serilog;

namespace AutomationFramework.Controls
{
    public class Control
    {
        private readonly ILogger _logger = LoggerProvider.GetLogger();
        private IWebDriver Driver { get; set; }
        private By? FindElementBy { get; set; }
        private IWebElement? _element;

        public Control(IWebDriver driver, By findElementBy)
        {
            Driver = driver;
            FindElementBy = findElementBy;
        }

        public Control(IWebDriver driver, IWebElement element, string elementDescription = "", string elementLocation = "")
        {
            Driver = driver;
            _element = element;
        }

        public IWebElement Element
        {
            get
            {
                if (FindElementBy != null)
                {
                    _element = FindElement(FindElementBy);
                }
                return _element!;
            }
        }

        public string Text => Element.Text;

        public bool IsExist
        {
            get
            {
                if (FindElementBy is null)
                {
                    throw new Exception("Control selector cannot be null");
                }

                var listIWebElements = FindElements(FindElementBy);
                return listIWebElements.Count > 0;
            }
        }

        public bool IsDisplayed => IsExist && Element.Displayed;

        public IEnumerable<T> GetAllControls<T>() where T : Control
        {
            if (FindElementBy is null)
            {
                throw new Exception("Control selector cannot be null");
            }

            var listIWebElements = FindElements(FindElementBy);
            var elements = listIWebElements.Select(p => (T)Activator.CreateInstance(typeof(T), Driver, p)!);

            return elements ?? Enumerable.Empty<T>();
        }

        public IWebElement FindElement(By elementLocator, int secondsToWait = 60)
        {
            var message = string.Empty;
            IWebElement? element = null;

            WaitHelper.WaitUntilCondition(() =>
            {
                bool isFound = false;

                try
                {
                    element = Driver.FindElement(elementLocator);
                    isFound = true;
                }
                catch (NoSuchElementException e)
                {
                    message = e.Message;
                }
                catch (StaleElementReferenceException e)
                {
                    message = e.Message;
                }
                catch (Exception e)
                {
                    message = e.Message;
                }

                return isFound;
            }, TimeSpan.FromSeconds(secondsToWait));

            if (element is null)
            {
                var log = $"It is impossible to find element by locator [{elementLocator.Criteria}] in {secondsToWait} seconds. \nError message: {message}";
                _logger.Error(log);
                throw new Exception(log);
            }

            return element;
        }

        public IList<IWebElement> FindElements(By elementLocator, IWebElement? parent = null)
        {
            IList<IWebElement> list = [];
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);

            try
            {
                list = parent == null ? Driver.FindElements(elementLocator) : parent.FindElements(elementLocator);
                return list;
            }
            catch (WebDriverException e)
            {
                _logger.Error($"{nameof(FindElements)} thrown an exception. \nException type {nameof(WebDriverException)}. \nException message: {e.Message}");
            }
            catch (Exception e)
            {
                _logger.Error($"{nameof(FindElements)} thrown an exception. \nException type {nameof(Exception)}. \nException message: {e.Message}");
            }

            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(ApplicationSettings.ImplicitWaitTimeout);
            return list;
        }

        public void Click()
        {
            var message = string.Empty;
            var scrolledToElement = false;
            var result = WaitHelper.WaitUntilCondition(() =>
            {
                try
                {
                    Element.Click();

                    return true;
                }
                catch (ElementClickInterceptedException e)
                {
                    message = e.GetType().FullName + " - " + e.Message;
                    if (!scrolledToElement)
                    {
                        Scroll();
                        scrolledToElement = true;
                    }
                    else
                    {
                        ScrollUp();
                    };
                }
                catch (ElementNotInteractableException e)
                {
                    message = e.GetType().FullName + " - " + e.Message;
                    _logger.Error($"{nameof(Click)}: Clicking element throws {nameof(ElementNotInteractableException)} with message: {message}.");
                }
                catch (StaleElementReferenceException e)
                {
                    message = e.GetType().FullName + " - " + e.Message;
                    _logger.Error(e, $"{nameof(Click)}: Clicking element throws {nameof(StaleElementReferenceException)} with message: {message}.");
                }
                catch (Exception e)
                {
                    message = e.GetType().FullName + " - " + e.Message;
                    _logger.Error(e, $"{nameof(Click)}: Clicking element throws {nameof(Exception)} with message: {message}.");
                }

                return false;
            }, TimeSpan.FromSeconds(60));

            if (!result)
            {
                throw new Exception(message);
            }
        }

        public virtual void SendKeys(string value, bool isNeedClearBefore = true)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            if (isNeedClearBefore)
            {
                Clear();
            }

            Element.SendKeys(value);
        }

        public void WaitForElementVisible(int waitInSeconds = 10)
        {
            _logger.Information($"{nameof(WaitForElementVisible)}: Waiting until element will be visible. Wait time is: {waitInSeconds} seconds.");

            try
            {
                if (FindElementBy != null)
                {
                    WaitHelper.WaitUntilCondition(() => ExpectedConditions.ElementIsVisible(FindElementBy), TimeSpan.FromSeconds(waitInSeconds));
                }
            }
            catch (Exception ex)
            {
                var log = $"{nameof(WaitForElementVisible)} has exception: {ex.Message}";
                _logger.Error(log);
                throw new Exception(log);
            }
        }        

        private void Clear()
        {
            Element.SendKeys(Keys.Control + "A");
            Element.SendKeys(Keys.Delete);
        }

        private void Scroll()
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)Driver;
            executor.ExecuteScript("arguments[0].scrollIntoView(true);", Element);
        }

        private void ScrollUp()
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)Driver;
            executor.ExecuteScript("window.scrollBy(0,-250)");
        }
    }
}
