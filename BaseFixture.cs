using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SnowE2E.Test.Helper;

namespace SnowE2E.Test
{
    [TestFixture]
    public class BaseFixture : DriverHelper
    {
        [SetUp]
        public static void Setup()
        {
            driver = WebDriverFactory.CreateDriver(AppSettings.BrowserType, AppSettings.BrowserOptions);
            jsedriver = (IJavaScriptExecutor)driver;

        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}