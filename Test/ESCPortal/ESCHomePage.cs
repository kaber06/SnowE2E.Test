using NUnit.Framework;
using SnowE2E.Test.Helper;
using SnowE2E.Test.Pages;

namespace SnowE2E.Test.Test.ESCPortal
{
    public class ESCHomePage : BaseFixture
    {
        [Test]
        public void ESCHP001_WidgetsShouldAvailable_ToEmployee()
        {
            var expectedQuickLinkCount = 3;
            string[] expectedWidgetNames = { "Email accounts", "Software issues", "Accessories", "Computers", "Software", "Developer services", "Technology services", "Phone and mobility" };

            LoginHelper loginHelper = new();
            loginHelper.LoginToSnow();

            driver.Navigate().GoToUrl(AppSettings.ESCHomePage);
            driverWait.Until(e => e.PageSource.Contains("Employee Center"));

            Pages.ESCHomePage escHomePage = new();
            Assert.That(escHomePage.WidgetNames, Is.EquivalentTo(expectedWidgetNames));
            Assert.That(escHomePage.QuickLinkNames.Count, Is.EqualTo(expectedQuickLinkCount));
        }

        [Test]
        public void ESCHP002_User_ClicksOnWidget_Successfull()
        {        
            LoginHelper loginHelper = new();
            loginHelper.LoginToSnow();

            driver.Navigate().GoToUrl(AppSettings.ESCHomePage);
            driverWait.Until(e => e.PageSource.Contains("Employee Center"));

            Pages.ESCHomePage escHomePage = new();
            escHomePage.ClickOnWidget("Email accounts");
            
            Assert.That(driver.PageSource.Contains("Email accounts"), Is.True);
        }
    }
}