using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace csharp_example_1
{

    [TestFixture]
    public class EnterNewProduct
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void TestMethod1()
        {

            driver.Url = "http://localhost/litecart/admin/.";
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Name("login")));
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            wait.Until(ExpectedConditions.TitleContains("My Store"));
            driver.Url = "http://localhost/litecart/admin/?app=catalog&doc=catalog&category_id=1";


            
            
            IList<IWebElement> elements = driver.FindElements(By.XPath("//img/following-sibling::a[1]"));
            List<string> links = new List<string>();

            foreach (IWebElement el in elements) {
                links.Add(el.GetAttribute("href"));
            }

            foreach (string link in links) {
                driver.Url = link;
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//h1")));
                Each(driver.Manage().Logs.GetLog("browser"), i => Assert.IsNull(i));                
            }

        }
        public void Each<T>(IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
                action(item);
        }
        public bool IsElementPresent(IWebDriver driver, By locator)
        {
            try
            {
                driver.FindElement(locator);
                return true;
            }
            catch (NoSuchElementException ex)
            {
                return false;
            }
        }    

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
