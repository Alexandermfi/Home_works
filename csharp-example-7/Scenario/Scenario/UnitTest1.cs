using System;

using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace csharp_example_1
{

    [TestFixture]
    public class LoginScenarioIe

    {
        private ChromeDriver driver;
        private WebDriverWait wait;
        //$$("ul#box-apps-menu>li")

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
            driver.Url = "http://localhost:/litecart/admin/.";
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Name("login")));
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@title='My Store']")));

            int count = driver.FindElements(By.XPath("//ul[@id='box-apps-menu']/li")).Count;

            for (int i = 1; i <= count; i++)
            {
                string locator = "//ul[@id='box-apps-menu']/li[" + i + "]";
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(locator)));
                driver.FindElement(By.XPath(locator)).Click();

                if (AreElementsPresent(driver, By.XPath(locator + "/ul"))){

                    int subCount = driver.FindElements(By.XPath(locator + "//li")).Count;

                    for (int j = 1 ; j <= subCount; j++) {
                        string sublocator = locator + "//li[" + j + "]";
                        wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(sublocator)));
                        driver.FindElement(By.XPath(sublocator)).Click();

                        wait.Until(ExpectedConditions.ElementIsVisible(By.Id("content")));
                        Assert.IsTrue(IsElementPresent(driver, By.CssSelector("h1")));
                       
                    }

                }
            }

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

        public bool AreElementsPresent(IWebDriver driver, By locator)
        {
            return driver.FindElements(locator).Count > 0;
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;

        }
    }
}
