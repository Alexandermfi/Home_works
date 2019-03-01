using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace csharp_example_1
{

    [TestFixture]
    public class CountryScenario

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
            driver.Url = "http://localhost:/litecart/admin/.";
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Name("login")));
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@title='My Store']")));


            driver.FindElement(By.XPath("//span[contains(.,'Countries')]")).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("content")));

            IList<IWebElement> Rows = driver.FindElements(By.CssSelector("td:nth-child(5) a")); 

            Rows[new Random().Next(0, Rows.Count)].Click();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//td[@id='content']")));

            IList<IWebElement> links = driver.FindElements(By.XPath("//i[@class='fa fa-external-link']/parent::*"));

            string mainWindow = driver.CurrentWindowHandle;

            int beforeClickWindowCount = driver.WindowHandles.ToList().Count;

            foreach (IWebElement el in links) {
                el.Click();  
                
                wait.Until(driver => driver.WindowHandles.Count == (beforeClickWindowCount + 1));

                foreach (string handle in driver.WindowHandles.ToList()) {
                    if (mainWindow != handle) {                        
                        driver.SwitchTo().Window(handle).Close();                  
                        driver.SwitchTo().Window(mainWindow);
                    }

                }
               
               
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
