using NUnit.Framework;
using SnowE2E.Test.Helper;
using System;

namespace SnowE2E.Test
{
    [SetUpFixture]
    public class GlobalFixture : DriverHelper
    {
        [OneTimeSetUp]
        public void GlobalSetup()
        {
            AppSettingsHelper.PopulateAppSettings();
        }

        [OneTimeTearDown]
        public void GlobalTeardown()
        {
            driver.Dispose();
        }
    }
}