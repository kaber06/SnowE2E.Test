using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowE2E.Test.Helper
{
    public class ShadowDOMHelper : DriverHelper
    {
        public static IWebElement GetNestedShadowElement(IWebElement root, string[] selectors)
        {
            object current = root;

            for (int i = 0; i < selectors.Length; i++)
            {
                string selector = selectors[i];
                current = jsedriver.ExecuteScript("return arguments[0].shadowRoot.querySelector(arguments[1]);", current, selector);

                if (current == null)
                {
                    throw new Exception($"Element not found at shadow step {i}: {selector}");
                }
            }

            return (IWebElement)current;
        }

    }
}