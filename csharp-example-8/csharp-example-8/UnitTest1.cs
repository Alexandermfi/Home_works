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

            IWebElement topProducts = driver.FindElement(By.Id("box-most-popular"));
            IWebElement centerProducts = driver.FindElement(By.Id("box-campaigns"));
            IWebElement bottomProducts = driver.FindElement(By.Id("box-latest-products"));

            IList<IWebElement> topListProducts = topProducts.FindElements(By.CssSelector("li"));

            foreach (IWebElement el in topListProducts) {
                bool trigger = false;
                if (IsElementPresent(el, By.CssSelector("div.sticker.new")))
                {
                    trigger = true;
                }
                else if (IsElementPresent(el, By.CssSelector("div.sticker.sale")))
                {
                    trigger = true;
                }
                else {
                    trigger = false;
                }
                Assert.IsTrue(trigger);
                
            }

            IList<IWebElement> centerListProducts = centerProducts.FindElements(By.CssSelector("li"));
            foreach (IWebElement el in centerListProducts)
            {
                bool trigger = false;
                if (IsElementPresent(el, By.CssSelector("div.sticker.new")))
                {
                    trigger = true;
                }
                else if (IsElementPresent(el, By.CssSelector("div.sticker.sale")))
                {
                    trigger = true;
                }
                else
                {
                    trigger = false;
                }
                Assert.IsTrue(trigger);
            }


            IList<IWebElement> bottomListProducts = bottomProducts.FindElements(By.CssSelector("li"));
            foreach (IWebElement el in bottomListProducts)
            {
                bool trigger = false;
                if (IsElementPresent(el, By.CssSelector("div.sticker.new")))
                {
                    trigger = true;
                }
                else if (IsElementPresent(el, By.CssSelector("div.sticker.sale")))
                {
                    trigger = true;
                }
                else
                {
                    trigger = false;
                }
                Assert.IsTrue(trigger);
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
