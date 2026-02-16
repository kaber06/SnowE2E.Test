using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnowE2E.Test.Helper;

namespace SnowE2E.Test.Pages
{
    public class ESCHomePage : DriverHelper
    {
        public List<string> WidgetNames { get; set; }
        public List<string> QuickLinkNames { get; set; }


        // Page elements and methods for the ESCHomePage
        private IList<IWebElement> popularWidgets => driver.FindElements(By.XPath("//div[@class='popular-topic-body-container ng-scope']/div"));
        private IWebElement quickLink => driver.FindElement(By.XPath("//div[contains(@class,'link-container panel')]"));
        private IList<IWebElement> quickLinkWidget => quickLink.FindElements(By.XPath("./div[2]//a"));
        private IList<IWebElement> widgets { get; set; }


        internal ESCHomePage()
        {
            Thread.Sleep(2000);
            WidgetNames = new List<string>();
            QuickLinkNames = new List<string>();
            widgets = new List<IWebElement>();

            GetWidgetNames();
            GetQuickLinks();
        }
        public void ClickOnQuickLink(string linkName)
        {
            var link = quickLinkWidget.FirstOrDefault(l => l.Text == linkName);
            if (link != null)
            {
                link.Click();
            }
        }
        private void GetWidgetNames()
        {
           
            foreach (var widget in popularWidgets)
            {
                if (widget.Displayed)
                {
                     WidgetNames.Add(widget.Text);
                }
               
            }
        }

        public void ClickOnWidget(string widgetName)
        {
            
            var widget = widgets.FirstOrDefault(w => w.FindElement(By.XPath(".//div[@class='popular-topic-title ng-binding']")).Text == widgetName);
            if (widget != null)
            {
                widget.Click();
            }

            driverWait.Until(e => e.PageSource.Contains(widgetName));
        }

        private void GetQuickLinks()
        {
            foreach (var link in quickLinkWidget)
            {
                var linkName = link.Text;
                QuickLinkNames.Add(linkName);
            }
        }

    }
}