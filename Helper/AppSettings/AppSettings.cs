using Microsoft.Extensions.Configuration;
using SnowE2E.Test.Helper;
using System;
using System.Xml;

namespace SnowE2E.Test.Helper
{
    public static class AppSettings
    {
        public static string MainUsername { get; set; } = null!;
        public static string MainPassword { get; set; } = null!;
        public static string Environment { get; set; } = null!;
        public static string ESCHomePage { get; set; } = null!;
        public static string BackOfficePage { get; set; } = null!;

        public static int ShortWaitTimeout { get; set; }
        public static BrowserType BrowserType { get; set; }
        public static string[] BrowserOptions { get; set; }
        public static TestRunType TestRunType { get; set; }

    }

    public enum TestRunType
    {
        local,
        gha
    }
}