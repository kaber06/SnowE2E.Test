using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using SnowE2E.Test.Modal;

namespace SnowE2E.Test.Helper
{
    class LoginHelper : DriverHelper
    {
        public void LoginToSnow(bool admin = true)
        {
            if (admin)
            {
                LoginAsAdmin();
            }
            else
            {
                driver.Navigate().GoToUrl(AppSettings.ESCHomePage);
                driverWait.Until(e => e.PageSource.Contains("Employee Center"));
            }
        }

        public void LoginToSNow(string impersonateeName, string impersonateeUserID, bool redirectToESCHomePage = false)
        {
            LoginAsAdmin();
            LoginAsImpersonatee(impersonateeUserID);

            if (redirectToESCHomePage)
            {
                driverWait.Until(e => e.PageSource.Contains("Employee Center"));
            }
            else
            {
                try
                {
                    Pages.BackOffice backOffice = new Pages.BackOffice();
                    Thread.Sleep(500); //a short sleep for the url change
                    driverWait.Until(e => jsedriver.ExecuteScript("return document.readyState").ToString() == "complete");
                }
                catch (Exception ex) { Console.WriteLine("Exception caught: {0}", ex); }
            }
        }

        private void EnterCredentials(string username, string password)
        {
            IWebElement usernameElement = driver.FindElement(By.Id("user_name"));
            driverWait.Until(e => usernameElement.Displayed);
            usernameElement.SendKeys(username);

            IWebElement passwordElement = driver.FindElement(By.Id("user_password"));
            passwordElement.SendKeys(password);
        }

        private void LoginAsAdmin()
        {
            string snowPass = Environment.GetEnvironmentVariable("SNOW_PASSWORD");
            string snowUsername = Environment.GetEnvironmentVariable("SNOW_USERNAME");
            driver.Navigate().GoToUrl(AppSettings.BackOfficePage + "/login.do");
            string title = driver.Title;

            Console.WriteLine(title);

            string username = AppSettings.TestRunType == TestRunType.local ? AppSettings.MainUsername : snowUsername;
            string password = AppSettings.TestRunType == TestRunType.local ? AppSettings.MainPassword : snowPass;

            EnterCredentials(username, password);

            //MainUser login here
            IWebElement loginBtn = driver.FindElement(By.Id("sysverb_login"));
            loginBtn.Click();
            Thread.Sleep(3000);

        }
        
        private void LoginAsImpersonatee(string impersonateeUserID)
        {
            Pages.BackOffice backOffice = new Pages.BackOffice();
            backOffice.ClickUserInfo();
            backOffice.ClickImpersonate();

            ImpersonateDialogBox impersonateDialogBox = new ImpersonateDialogBox();
            impersonateDialogBox.EnterID(impersonateeUserID);
            impersonateDialogBox.ClickUser();
            Thread.Sleep(3000);
        }
    }
}