using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace csharp_example_1
{

    [TestFixture]
    public class StickersTest

    {
        private ChromeDriver driver;
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
            driver.Url = "http://localhost/litecart/en/";
            
            wait.Until(ExpectedConditions.TitleContains("Online Store | My Store"));

            //div.image - wrapper > div.sticker

            IList<IWebElement> products = driver.FindElements(By.CssSelector("li.product"));           
            
            foreach (IWebElement el in products) {
                   Assert.IsTrue(IsElementPresent(el, By.CssSelector("div.sticker")));
            }

        }



        public bool IsElementPresent(IWebElement element, By locator)
        {
            try
            {
                element.FindElement(locator);
                return true;
            }
            catch (NoSuchElementException ex)
            {
                return false;
            }
        }

        public bool AreElementsPresent(IWebElement driver, By locator)
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
