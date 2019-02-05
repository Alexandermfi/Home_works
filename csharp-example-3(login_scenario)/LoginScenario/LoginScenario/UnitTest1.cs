using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace csharp_example_1
{

    [TestFixture]
    public class LoginScenario
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void TestMethod1()
        {
            driver.Url = "http://localhost/litecart/admin/.";
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Name("login")));

            IWebElement usernameFiled = driver.FindElement(By.Name("username"));
            usernameFiled.SendKeys("admin");

            IWebElement passwordField = driver.FindElement(By.Name("password"));
            passwordField.SendKeys("admin");

            IWebElement loginButton = driver.FindElement(By.Name("login"));
            loginButton.Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@title='My Store']")));
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
