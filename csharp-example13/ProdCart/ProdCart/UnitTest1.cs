using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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

                if (IsElementPresent(driver, By.Name("options[Size]")))
                {
                    wait.Until(ExpectedConditions.ElementIsVisible(By.Name("options[Size]")));
                    new SelectElement(driver.FindElement(By.Name("options[Size]"))).SelectByIndex(1);
                }              

                driver.FindElement(By.CssSelector("button[name=\"add_cart_product\"]")).Click();

                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[@class='quantity'][contains(.," + (i+1)+ ")]")));
             
                driver.FindElement(By.Id("logotype-wrapper")).Click();
              
               
            }
            
            driver.FindElement(By.XPath("//a[contains(.,'Checkout »')]")).Click();

            IWebElement table =driver.FindElement(By.Id("order_confirmation-wrapper"));


            for (int i = 0; i < productCount; i++) {
                wait.Until(ExpectedConditions.ElementIsVisible(By.Id("order_confirmation-wrapper")));
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[contains(.,'Remove')]")));
                driver.FindElement(By.XPath("//button[contains(.,'Remove')]")).Click();

                if (isElementNotExist(table)) {
                    driver.FindElement(By.XPath("//button[contains(.,'Remove')]")).Click();
                    table = driver.FindElement(By.Id("order_confirmation-wrapper"));
                }          
             
            }

        }      


        public bool WaitElementBy(IWebDriver driver, By locator)
        {
            try
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                return driver.FindElements(locator).Count > 0;
            }
            finally
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);

            }
        }

        public bool isElementNotExist(IWebElement el) {
            try
            {
                el.Click();
                return false;
            }
            catch (StaleElementReferenceException ex)
            {
                return true;
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
