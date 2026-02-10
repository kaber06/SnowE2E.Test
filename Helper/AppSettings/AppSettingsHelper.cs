using Microsoft.Extensions.Configuration;
using WebDriverManager.Services.Impl;

namespace SnowE2E.Test.Helper
{
    public static class AppSettingsHelper
    {
        public static void PopulateAppSettings()
        {
            var AppSettingsFile = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            AppSettings.Environment = AppSettingsFile.GetValue<string>("testrunner:environment");
            var envSection = AppSettingsFile.GetSection("servicenow:" + AppSettings.Environment);
            AppSettings.ESCHomePage = AppSettingsFile.GetValue<string>(envSection.Path + ":ESC");
            AppSettings.BackOfficePage = AppSettingsFile.GetValue<string>(envSection.Path + ":BackOfficeHomePage");

            AppSettings.MainUsername = AppSettingsFile.GetValue<string>("servicenow:mainUser:username");
            AppSettings.MainPassword = AppSettingsFile.GetValue<string>("servicenow:mainUser:password");
            AppSettings.ShortWaitTimeout = AppSettingsFile.GetValue<int>("testrunner:ShortWaitTimeout");
            AppSettings.TestRunType = AppSettingsFile.GetValue<TestRunType>("testrunner:TestRunType");
            AppSettings.BrowserType = WebDriverFactory.GetBrowserTypeByName(AppSettingsFile.GetValue<string>("browser:name"));
            AppSettings.BrowserOptions = AppSettingsFile.GetSection("browser:options").Get<string[]>();
            
        }
        
    }
 }
