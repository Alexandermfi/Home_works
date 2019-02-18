using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace csharp_example_1
{

    [TestFixture]
    public class CountryScenario

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
            driver.Url = "http://localhost:/litecart/admin/.";
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Name("login")));
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@title='My Store']")));


            driver.FindElement(By.XPath("//span[contains(.,'Countries')]")).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("content")));

            IList<IWebElement> Rows = driver.FindElements(By.CssSelector(".row"));

            List<string> unsortedList, sortedList = new List<string>();
            List<string> zoneUrls = new List<string>();
            

            foreach (IWebElement el in Rows) {
                sortedList.Add(el.FindElement(By.CssSelector("td:nth-child(5)")).GetAttribute("textContent"));
                if (!(el.FindElement(By.CssSelector("td:nth-child(6)")).Text.Equals("0")))
                {
                    zoneUrls.Add(el.FindElement(By.CssSelector("td:nth-child(5) a")).GetAttribute("href"));
                }           
            }

            unsortedList = sortedList;
            sortedList.Sort();
            Assert.IsTrue(unsortedList.SequenceEqual(sortedList));
       

            foreach (string link in zoneUrls) {
                driver.Url = link;
                List<string> sortedZoneList, zoneList = new List<string>();
                
                List<IWebElement> zoneRows = new List<IWebElement>(driver.FindElements(By.CssSelector("table#table-zones tr")));

                foreach (IWebElement el in zoneRows)
                {
                    if (IsElementPresent(el, By.CssSelector("td:nth-child(3)"))
                        &&
                        (el.FindElement(By.CssSelector("td:nth-child(3)")).GetAttribute("textContent") != ""))
                    {
                        zoneList.Add(el.FindElement(By.CssSelector("td:nth-child(3)")).GetAttribute("textContent"));
                    }                               
                   
                }
                sortedZoneList = zoneList;
                zoneList.Sort();
                Assert.IsTrue(zoneList.SequenceEqual(sortedZoneList));

            }
                  

        }
        [Test]
        public void TestMethod2()
        {
            driver.Url = "http://localhost:/litecart/admin/.";
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Name("login")));
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@title='My Store']")));


            driver.FindElement(By.XPath("//span[contains(.,'Geo Zones')]")).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("content")));

            IList<IWebElement> Rows = driver.FindElements(By.CssSelector(".row"));
            
            List<string> zoneUrls = new List<string>();

            foreach (IWebElement el in Rows)
            {             
                    zoneUrls.Add(el.FindElement(By.CssSelector("a")).GetAttribute("href"));               
            }

            foreach (string link in zoneUrls)
            {
                driver.Url = link;

                List<string> zoneList= new List<string>();
                List<string> sortedZoneList = new List<string>();

                List<IWebElement> zoneRows = new List<IWebElement>(driver.FindElements(By.CssSelector("select[name*='zone_code']")));

                foreach (IWebElement el in zoneRows)
                {
                    
                    SelectElement selectedValue = new SelectElement(el);
                    sortedZoneList.Add(selectedValue.SelectedOption.Text);
                }
                sortedZoneList = zoneList;
                sortedZoneList.Sort();
                Assert.IsTrue(zoneList.SequenceEqual(sortedZoneList));
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

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;

        }
    }
}
