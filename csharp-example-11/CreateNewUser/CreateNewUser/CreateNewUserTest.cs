using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;


namespace csharp_example_11
{

    [TestFixture]
    public class LoginScenarioIe

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
            string prefix = DateTime.Now.ToString("HH_mm_ss_fff");
            string email = "email" + prefix + "@email.ru";
            string password = "123456";
            string company = "Company" + prefix;
            string username = "User" + prefix;
            string lastname = "LastName" + prefix;
            string addres1 = "Addres1" + prefix;
            string addres2 = "Addres2" + prefix;
            string city = "NewYork";
            string phone = "+7 333 555 2222";
            string country = "United States";
           
            driver.Url = "http://localhost:/litecart/en/";
            
            driver.FindElement(By.XPath("//a[contains(., 'New customers click here')]")).Click();
            driver.FindElement(By.Name("tax_id")).SendKeys(prefix);
            driver.FindElement(By.Name("company")).SendKeys(company);
            driver.FindElement(By.Name("firstname")).SendKeys(username);
            driver.FindElement(By.Name("lastname")).SendKeys(lastname);
            driver.FindElement(By.Name("address1")).SendKeys(addres1);
            driver.FindElement(By.Name("address2")).SendKeys(addres2);
            //postcode
            driver.FindElement(By.Name("postcode")).SendKeys(new Random().Next(10000,99999).ToString());
            driver.FindElement(By.Name("city")).SendKeys(city);
            //Select
            driver.FindElement(By.ClassName("select2-selection__arrow")).Click();
            driver.FindElement(By.ClassName("select2-search__field")).SendKeys(country);
            new Actions(driver).SendKeys(Keys.Enter).Perform();         
            driver.FindElement(By.Name("email")).SendKeys(email);
            driver.FindElement(By.Name("phone")).SendKeys(phone);
 
            IWebElement zoneSelector = driver.FindElement(By.XPath("//select[@name='zone_code']"));

            if (zoneSelector.GetAttribute("disabled").Equals("true"))
            {
                driver.FindElement(By.Name("password")).SendKeys(password);
                driver.FindElement(By.Name("confirmed_password")).SendKeys(password);
                driver.FindElement(By.Name("create_account")).Click();

                //create new element after page update
                SelectElement el = new SelectElement(driver.FindElement(By.XPath("//select[@name='zone_code']")));
                el.SelectByIndex(new Random().Next(0, el.Options.Count - 1));
            }
            else {
                SelectElement el = new SelectElement(zoneSelector);
                el.SelectByIndex(new Random().Next(0, el.Options.Count - 1));
            }

            driver.FindElement(By.Name("password")).SendKeys(password);
            driver.FindElement(By.Name("confirmed_password")).SendKeys(password);
            driver.FindElement(By.Name("create_account")).Click();
           
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[contains(.,'Logout')]")));
            driver.FindElement(By.XPath("//a[contains(.,'Logout')]")).Click();
            driver.FindElement(By.Name("email")).SendKeys(email);
            driver.FindElement(By.Name("password")).SendKeys(password);
            new Actions(driver).SendKeys(Keys.Enter).Perform();

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[contains(.,'Logout')]")));
            driver.FindElement(By.XPath("//a[contains(.,'Logout')]")).Click();
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