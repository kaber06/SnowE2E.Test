using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using System.Threading;
using SnowE2E.Test.Helper;

namespace SnowE2E.Test;

public class Tests : DriverHelper
{

    [SetUp]
    public void Setup()
    {
        driver = new ChromeDriver();
    }
    
    [Ignore("This is a sandbox test, not meant to be run in CI/CD")]
    [Test]
    public void NavigateToGoogle()
    {
        driver.Navigate().GoToUrl("https://www.google.com");
        Assert.That(driver.Title, Does.Contain("Google"));
        Thread.Sleep(5000);
    }

    [TearDown]
    public void TearDown()
    {
          driver.Quit();
          driver.Dispose();
    }
}
