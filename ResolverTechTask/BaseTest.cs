using ApplicationData.Pages;
using AutomationFramework.Providers;
using Bogus;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using Serilog;

namespace ResolverTechTask
{
    [TestFixture]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    public abstract class BaseTest
    {
        protected IWebDriver Driver;
        protected PageFacade Pages;
        protected ILogger Logger;
        protected Faker Fake = new();

        public BaseTest()
        {
            SettingsProvider.ReadSettings();
            Driver = DriverProvider.InitializeChromeDriver();
            Pages = new PageFacade(Driver);
            Logger = LoggerProvider.GetLogger();
        }

        [TearDown]
        public virtual void TearDown()
        {
            AddCurrentPageScreenshot();

            try
            {
                Driver?.Dispose();
                Driver?.Quit();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Can not Dispose or Quit driver.");
            }
        }

        private void AddCurrentPageScreenshot()
        {
            if (TestContext.CurrentContext.Result.Outcome == ResultState.Success)
            {
                Logger.Information($"----------Test {TestContext.CurrentContext.Test.Name} - {TestContext.CurrentContext.Result.Outcome.Status}.----------");
                return;
            }

            Logger.Error($"----------Test {TestContext.CurrentContext.Test.Name} - {TestContext.CurrentContext.Result.Outcome.Status}.----------");
            Logger.Information("\"----------Adding screenshot to a test.----------");

            Screenshot screenshot = ((ITakesScreenshot)Driver).GetScreenshot();

            var methodName = TestContext.CurrentContext.Test.Name;
            var testResults = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestResults");

            if (!Directory.Exists(testResults))
            {
                Directory.CreateDirectory(testResults);
            }

            var screenshotPath = Path.Combine(testResults, $"{methodName}_{DateTime.Now.Ticks}.jpg");

            try
            {
                screenshot.SaveAsFile(screenshotPath);
                TestContext.AddTestAttachment(screenshotPath);
                Logger.Information("----------Screenshot added to a test.----------");
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception occured when trying to add screenshot. \nException message: {ex.Message}");
            }
        }
    }
}
