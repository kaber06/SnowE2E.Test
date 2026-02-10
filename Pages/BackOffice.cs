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
            "span > span > span"
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

        string[] RITMNumberSuffix = new[]
        {
            "#title-tooltip"
        };

        string[] exactSearchSuffix = new[]
        {
            "div>sn-canvas-appshell-root>sn-canvas-appshell-layout>sn-polaris-layout>div>sn-canvas-appshell-main",
            "macroponent-76a83a645b122010b913030a1d81c780",
            "sn-canvas-root>sn-canvas-layout>sn-canvas-main",
            "sn-canvas-screen:nth-child(2)",
            "screen-action-transformer-dcd3e42dc7202010099a308dc7c26002>macroponent-d4d3a42dc7202010099a308dc7c2602b",
            "now-uxf-page>div>sn-search-result-wrapper",
            "sn-component-workspace-global-search-tab",
            "div>div>div>div:nth-child(2)>ul"
        };

        IWebElement root => driver.FindElement(By.CssSelector("macroponent-f51912f4c700201072b211d4d8c26010"));
        IWebElement userInfo => ShadowDOMHelper.GetNestedShadowElement(root, (commonShadowPrefix.Concat(userInfoSuffix)).ToArray());
        IWebElement impersonateOption => ShadowDOMHelper.GetNestedShadowElement(root, (commonShadowPrefix.Concat(impersonateOptionSuffix)).ToArray());
        IWebElement globalSearch => ShadowDOMHelper.GetNestedShadowElement(root, (commonShadowPrefix.Concat(globalSearchSuffix)).ToArray());
        IWebElement RITMNumber => ShadowDOMHelper.GetNestedShadowElement(root, (commonShadowPrefix.Concat(RITMNumberSuffix)).ToArray());
        IWebElement exactSearch => ShadowDOMHelper.GetNestedShadowElement(root, exactSearchSuffix);


        internal IWebElement mainShadowRoot => (IWebElement)jsedriver.ExecuteScript("return document.querySelector(\"body > macroponent-f51912f4c700201072b211d4d8c26010\")");
        IList<IWebElement> dropzones => driver.FindElements(By.CssSelector("#homepage_grid > tbody > tr > td"));
        IList<IWebElement> catalogs { get; set; }
        IList<IWebElement> categories { get; set; }
        IList<IWebElement> catalogItem { get; set; }

        internal string iframename { get; set; }
        internal IDictionary<string, SingleFieldClass> formFieldDict { get; set; }
        #endregion

        public void ClickUserInfo() => userInfo.Click();
        public string GetUserInfoAttribute(string attribute) => userInfo.GetAttribute(attribute);
        public string GetRITMNumber(string keyword)
        {
            SwitchToIframe();

            IWebElement ritmNumber = driver.FindElement(By.Id("sys_readonly.sc_req_item.number"));
            try
            {
                driverWait.Until(e => ritmNumber.GetAttribute("value").Contains(keyword));
                return ritmNumber.GetAttribute("value").ToString();
            }
            catch { return string.Empty; }
        }

        public void ClickImpersonate()
        {
            Thread.Sleep(1000);
            impersonateOption.Click();
        }
        
        //Private methods
        internal void PopulateFieldDictionary(string label, string fieldtype, IWebElement element)
        {
            SingleFieldClass fieldClass = new SingleFieldClass();

            fieldClass.Label = label;
            fieldClass.FieldID = fieldtype;
            fieldClass.Element = element;

            formFieldDict.Add(key: fieldClass.Label, value: fieldClass);
        }

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

        internal class SingleFieldClass
        {
            public string Label { get; set; }
            public string FieldID { get; set; }
            public IWebElement Element { get; set; }
        }
    }
}