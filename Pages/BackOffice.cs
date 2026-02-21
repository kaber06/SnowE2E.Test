using OpenQA.Selenium;
using SnowE2E.Test.Helper;
using System;
using OpenQA.Selenium.Support.UI;

namespace SnowE2E.Test.Pages
{
    public class BackOffice : DriverHelper
    {
        #region private properties

        string[] commonShadowPrefix = new[]
        {
            "div > sn-canvas-appshell-root > sn-canvas-appshell-layout > sn-polaris-layout",
            "div.sn-polaris-layout.polaris-enabled > div.layout-main > div.header-bar > sn-polaris-header"
        };

        string[] userInfoSuffix = new[]
        {
            "nav > div > div.ending-header-zone > div.polaris-header-controls > div.utility-menu-container > div.utility-menu.can-animate > div > now-avatar",
            "span > span"
        };

        string[] impersonateOptionSuffix = new[]
        {
            "#userMenu > span > span:nth-child(2) > div > div.user-menu-controls > button.user-menu-button.impersonateUser.keyboard-navigatable.polaris-enabled"
        };

        string[] globalSearchSuffix = new[]
        {
            "nav > div > div.ending-header-zone > div.polaris-header-controls > div.search-container > div.polaris-search.polaris-enabled > sn-search-input-wrapper",
            "sn-component-workspace-global-search-typeahead",
            "#sncwsgs-typeahead-input"
        };

    
        IWebElement root => driver.FindElement(By.CssSelector("macroponent-f51912f4c700201072b211d4d8c26010"));
        IWebElement userInfo => ShadowDOMHelper.GetNestedShadowElement(root, (commonShadowPrefix.Concat(userInfoSuffix)).ToArray());
        IWebElement impersonateOption => ShadowDOMHelper.GetNestedShadowElement(root, (commonShadowPrefix.Concat(impersonateOptionSuffix)).ToArray());
        IWebElement globalSearch => ShadowDOMHelper.GetNestedShadowElement(root, (commonShadowPrefix.Concat(globalSearchSuffix)).ToArray());
        


        internal IWebElement mainShadowRoot => (IWebElement)jsedriver.ExecuteScript("return document.querySelector(\"body > macroponent-f51912f4c700201072b211d4d8c26010\")");
       
        internal string iframename { get; set; }
  
        #endregion

        public void ClickUserInfo() => userInfo.Click();
        public string GetUserInfoAttribute(string attribute) => userInfo.GetAttribute(attribute);

        public void ClickImpersonate()
        {
            Thread.Sleep(1000);
            impersonateOption.Click();
        }
        
        //Private methods

        private void SwitchToIframe()
        {
            if ((string)jsedriver.ExecuteScript("return self.name") != "gsft_main")
            {
                IWebElement iframe = mainShadowRoot.GetShadowRoot().FindElement(By.Id("gsft_main"));
                driver.SwitchTo().Frame(iframe);
                iframename = (string)jsedriver.ExecuteScript("return self.name");
            }
        }

        internal static void FindAndClickThisElement(string element, IList<IWebElement> elementCollection)
        {
            var elementFound = false;
            foreach (var singleElement in elementCollection)
            {
                if (element == singleElement.Text)
                //if (singleElement.Text.Contains(element))
                {
                    singleElement.Click();
                    elementFound = true;
                    break;
                }
            }
            if (!elementFound)
            {
                throw new NotFoundException($"The element: \"{element}\", is not found.");
            }
        }
    }
}