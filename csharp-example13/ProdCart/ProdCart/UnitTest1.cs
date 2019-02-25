using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace csharp_example_10_1
{

    [TestFixture]
    public class ProductCheckParams

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
            driver.Url = "http://litecart.stqa.ru/en/";

            int productCount = 3;

            for (int i = 0 ;i < productCount; i++) {
                driver.FindElement(By.CssSelector("li.product")).Click();

                addToCart();

                driver.FindElement(By.Id("logotype-wrapper")).Click();
            }

            driver.FindElement(By.XPath("//a[contains(.,'Checkout »')]")).Click();

            for (int i = 0; i < productCount; i++) {
                if (IsElementPresent(driver, By.XPath("//td[@class='item']"))) {
                    driver.FindElement(By.Name("remove_cart_item")).Click();
                    Thread.Sleep(500);
                }
            }

        }


        public void addToCart() {
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("h1.title")));

            int count = int.Parse(driver.FindElement(By.CssSelector("span[class=\"quantity\"]")).Text);
            int control = count;

            if (IsElementPresent(driver, By.Name("options[Size]")))
            {
                wait.Until(ExpectedConditions.ElementIsVisible(By.Name("options[Size]")));
                new SelectElement(driver.FindElement(By.Name("options[Size]"))).SelectByIndex(1);
            }
            
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("button[name=\"add_cart_product\"]")));
            driver.FindElement(By.CssSelector("button[name=\"add_cart_product\"]")).Click();
            control++;

            while (control!=count) {
                Thread.Sleep(100);
                count = int.Parse(driver.FindElement(By.CssSelector("span[class=\"quantity\"]")).Text);
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
