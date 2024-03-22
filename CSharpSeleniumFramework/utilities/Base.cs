using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;

namespace CSharpSeleniumFramework.utilities
{
    public class Base
    {
        public IWebDriver driver;
        public static jsonReader reader { get; set; } = new jsonReader();

        ExtentReports extent;
        ExtentTest test;

        [OneTimeSetUp]
        public void Setup()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            var htmlReporter = new ExtentHtmlReporter(projectDirectory + "//index.html");
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo("Host", "localhost");
            extent.AddSystemInfo("Environment", "QA");
            extent.AddSystemInfo("Username", "malagi");
        }


        [SetUp]
        public void StartBrowser()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);

            driver = new ChromeDriver("chromedriver.exe");

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            driver.Manage().Window.Maximize();
            string? actUrl = ConfigurationManager.AppSettings["Url"];
            driver.Url = actUrl;

        }

        [TearDown]
        public void StopBrowser()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace;
            string fileName = "Screenshot_" + DateTime.Now.ToString("H_mm_ss") + ".png";
            if (status == TestStatus.Failed)
            {
                test.Fail("Test failed", captureScreenShot(fileName));
                test.Log(Status.Fail, "Test failed with logtrace " + stackTrace);
            }
            extent.Flush();

            driver.Dispose();
        }

        public MediaEntityModelProvider captureScreenShot(string name)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            var screenshot = ts.GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, name).Build();

        }


    }
}
