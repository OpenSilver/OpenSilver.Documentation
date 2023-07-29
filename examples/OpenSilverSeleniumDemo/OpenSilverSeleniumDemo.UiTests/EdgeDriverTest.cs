using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics;
using System.Linq;

namespace OpenSilverSeleniumDemo.UiTests
{
    [TestClass]
    public class OpenSilverSeleniumTests
    {
        private const string dotnetPath = "C:\\Program Files\\dotnet\\dotnet.exe";
        private const string blazorServerPath = "path\\to\\.nuget\\blazor-devserver.dll";
        private const string appPath = "path\\to\\YourProject.Browser.dll";

        private const string appUrl = "https://localhost:55123";

        private EdgeDriver _driver;
        private static Process _server;
        private WebDriverWait _wait;

        #region Server Setup

        [AssemblyInitialize()]
        public static void ServerInitialize(TestContext testContext)
        {
            _server = Process.Start(
                dotnetPath,
                $"\"{blazorServerPath}\" --applicationpath \"{appPath}\" --urls={appUrl}");
        }

        [AssemblyCleanup]
        public static void TearDown()
        {
            _server?.Kill();
        }

        #endregion Server Setup

        #region Driver Setup

        [TestInitialize]
        public void EdgeDriverInitialize()
        {
            var options = new EdgeOptions();
            options.PageLoadStrategy = PageLoadStrategy.Normal;
            // uncomment this for CI/CD scenarios
            // options.AddArgument("headless");

            _driver = new EdgeDriver(options);
            _driver.Url = appUrl;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        [TestCleanup]
        public void EdgeDriverCleanup()
        {
            _driver.Quit();
        }

        #endregion Driver Setup

        [TestMethod]
        public void VerifyPageTitle()
        {
            Assert.AreEqual("OpenSilverSeleniumDemo", _driver.Title);
        }

        [TestMethod]
        public void VerifyAllControlsAreLoaded()
        {
            var label = _wait.Until(d => d.FindElement(By.XPath("//*[contains(text(),'Add Items')]")));

            var txtNewItem = _driver.FindElements(By.CssSelector("[dataid='txtNewItem']")).FirstOrDefault();
            var btnAdd = _driver.FindElements(By.CssSelector("[dataid='btnAdd']")).FirstOrDefault();
            var listItems = _driver.FindElements(By.CssSelector("[dataid='listItems']")).FirstOrDefault();

            Assert.IsNotNull(label, "Add Items label");
            Assert.IsNotNull(txtNewItem, "txtNewItem");
            Assert.IsNotNull(btnAdd, "btnAdd");
            Assert.IsNotNull(listItems, "listItems");
        }

        [TestMethod]
        public void VerifyWeCanAddAnItem()
        {
            const string sampleText = "OpenSilver Rocks!";

            // wait until the page is loaded
            _wait.Until(d => d.FindElement(By.XPath("//*[contains(text(),'Add Items')]")));

            // get element references
            var txtNewItem = _wait.Until(e => e.FindElement(By.CssSelector("[dataid='txtNewItem']")));
            var btnAdd = _wait.Until(e => e.FindElement(By.CssSelector("[dataid='btnAdd']")));
            var listItems = _wait.Until(e => e.FindElement(By.CssSelector("[dataid='listItems']")));

            // type the sample text in textbox
            txtNewItem
                .FindElement(By.CssSelector("[contenteditable='true']"))
                .SendKeys(sampleText);

            btnAdd.Click();

            var listItem = listItems.FindElements(By.XPath($"//*[contains(text(),'{sampleText}')]")).FirstOrDefault();

            Assert.IsNotNull(listItem);
        }
    }
}