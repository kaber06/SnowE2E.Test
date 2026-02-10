
﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;
using WebDriverManager;
using WebDriverManager.DriverConfigs;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace SnowE2E.Test.Helper
{
    public class WebDriverFactory
    {
        public static IWebDriver CreateDriver(BrowserType browserType, string[] additionalOptions = null)
        {
            IDriverConfig driverConfig = GetDriverConfig(browserType);
            new DriverManager().SetUpDriver(driverConfig, VersionResolveStrategy.MatchingBrowser);

            switch (browserType)
            {
                case BrowserType.Chrome:
                    var chromeOptions = new ChromeOptions();
                    if (additionalOptions != null) { }
                    chromeOptions.AddArguments(additionalOptions);

                    return new ChromeDriver(chromeOptions);
                case BrowserType.Firefox:
                    var firefoxOptions = new FirefoxOptions();
                    if (additionalOptions != null)
                        firefoxOptions.AddArguments(additionalOptions);

                    return new FirefoxDriver(firefoxOptions);
                case BrowserType.Edge:
                    var edgeOptions = new EdgeOptions();
                    if (additionalOptions != null)
                        edgeOptions.AddArguments(additionalOptions);
                    return new EdgeDriver(edgeOptions);
                default:
                    throw new ArgumentException("Unsupported browser type");
            }
        }

       
        public static BrowserType GetBrowserTypeByName(string browserName)
        {
            string lowerCaseBrowserName = browserName.ToLower();

            return lowerCaseBrowserName switch
            {
                "chrome" => BrowserType.Chrome,
                "firefox" => BrowserType.Firefox,
                "edge" => BrowserType.Edge,
                _ => throw new ArgumentException("Unsupported browser name"),
            };
        }

        //map the browser of choice to the specific configuration needed by the webdrivermanager
        private static IDriverConfig GetDriverConfig(BrowserType browserType)
        {
            return browserType switch
            {
                BrowserType.Chrome => new ChromeConfig(),
                BrowserType.Firefox => new FirefoxConfig(),
                BrowserType.Edge => new EdgeConfig(),
                _ => throw new ArgumentException("Unsupported browser type"),
            };
        }
    }

    //list of allowed browsers
    public enum BrowserType
    {
        Chrome,
        Firefox,
        Edge
    }
}
