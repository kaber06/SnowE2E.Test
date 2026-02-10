using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SnowE2E.Test.Helper;
using System;
using System.Linq;
using System.Threading;

namespace SnowE2E.Test.Modal
{
    class ImpersonateDialogBox : DriverHelper
    {
        string[] commonShadowPrefix = new[]
        {
            "div > sn-canvas-appshell-root > sn-canvas-appshell-layout > sn-polaris-layout",
            "div.sn-polaris-layout.polaris-enabled > div.layout-main > div.content-area.can-animate > sn-impersonation",
        };

        string[] nameFieldSuffix = new[]
        {
            "now-modal > div > now-typeahead",
            "input"
        };

        string[] impersonateButtonSuffix = new[]
        {
            "now-modal",
            "div > div > div > div.now-modal-footer > now-button:nth-child(2)",
            "button"
        };

        IWebElement root => driver.FindElement(By.CssSelector("macroponent-f51912f4c700201072b211d4d8c26010"));
        IWebElement nameField => ShadowDOMHelper.GetNestedShadowElement(root, (commonShadowPrefix.Concat(nameFieldSuffix)).ToArray());
    
        IWebElement userFound => (IWebElement)jsedriver.ExecuteScript("return document.querySelector(\"body > now-popover-panel > seismic-hoist\")" +
            ".shadowRoot.querySelector(\"div > div > div\")");

        public void EnterID(string value)
        {
            Thread.Sleep(3000);
            nameField.SendKeys(value);
        }

        public void ClickUser()
        {
            Thread.Sleep(1500);
            userFound.Click();

            IWebElement impersonateButton = ShadowDOMHelper.GetNestedShadowElement(root, (commonShadowPrefix.Concat(impersonateButtonSuffix)).ToArray());

            impersonateButton.Click();

        }
    }
}