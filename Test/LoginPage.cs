using NUnit.Framework;
using SnowE2E.Test.Helper;

namespace SnowE2E.Test.Test
{
    public class LoginPage : BaseFixture
    {
        [Test]
        public void LP001_LoginTest()
        {
            LoginHelper loginHelper = new();
            loginHelper.LoginToSnow();

            Pages.BackOffice backOffice = new();
            Assert.That((driver.Title).Contains("ServiceNow"));
        }
    }
}