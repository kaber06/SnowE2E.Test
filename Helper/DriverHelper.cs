using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using Newtonsoft.Json;

namespace SnowE2E.Test.Helper
{
    public class DriverHelper
    {
        public static IWebDriver driver { get; set; } = null!;
        public static IJavaScriptExecutor jsedriver { get; set; } = null!;

        public static WebDriverWait driverWait => new(driver, TimeSpan.FromSeconds(AppSettings.ShortWaitTimeout));

    }
}