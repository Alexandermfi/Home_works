using System;

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
        private InternetExplorerDriver driver;
        private WebDriverWait wait;
        InternetExplorerOptions options = new InternetExplorerOptions();



        [SetUp]
        public void start()
        {
            options.RequireWindowFocus = true;
            driver = new InternetExplorerDriver(options);

            //driver.Manage().Window.Maximize();

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void TestMethod1()
        {
            driver.Url = "http://localhost:/litecart/en/";

            string mainName = "";
            int mainOldPrice = 0;
            int mainNewPrice = 0;


            IWebElement campaings = driver.FindElement(By.Id("box-campaigns"));

            mainName = campaings.FindElement(By.ClassName("name")).Text;

            //Old price are grey               
            Assert.IsTrue(isColorGrey(campaings.FindElement(By.ClassName("regular-price"))));
            //Old price is line-through	
            Assert.IsTrue(campaings.FindElement(By.ClassName("regular-price")).GetCssValue("text-decoration").Contains("line-through"));

            //New price are red
            Assert.IsTrue(isColorRed(campaings.FindElement(By.ClassName("campaign-price"))));
            //New price is strong==(bold, >700, etc)
            Assert.IsTrue(campaings.FindElement(By.ClassName("campaign-price")).GetAttribute("tagName").Contains("STRONG"));

            mainOldPrice = int.Parse(campaings.FindElement(By.ClassName("regular-price")).Text.Replace("$", ""));
            mainNewPrice = int.Parse(campaings.FindElement(By.ClassName("campaign-price")).Text.Replace("$", ""));

            double oldPriceFontSize = Convert.ToDouble(
                campaings.FindElement(By.ClassName("regular-price"))
                .GetCssValue("font-size")
                .Replace("px", "")
                .Replace(".", ","));
            double newPriceFontsize = Convert.ToDouble(
                campaings.FindElement(By.ClassName("campaign-price"))
                .GetCssValue("font-size")
                .Replace("px", ""));

            //Old price font-size less then new price font-size
            Assert.IsTrue(newPriceFontsize > oldPriceFontSize);

            //Open Product page
            campaings.FindElement(By.CssSelector("li.product")).Click();

            //Name in main page equals name in product page
            Assert.IsTrue(mainName.Equals(driver.FindElement(By.CssSelector("h1.title")).Text));

            //Old price in main page equals old price in product page
            Assert.IsTrue(mainOldPrice.Equals(int.Parse(driver.FindElement(By.ClassName("regular-price")).Text.Replace("$", ""))));

            //New price in main page equals new price in product page
            Assert.IsTrue(mainNewPrice.Equals(int.Parse(driver.FindElement(By.ClassName("campaign-price")).Text.Replace("$", ""))));

            oldPriceFontSize = Convert.ToDouble(
                driver.FindElement(By.ClassName("regular-price"))
                .GetCssValue("font-size")
                .Replace("px", "")
                .Replace(".", ","));

            newPriceFontsize = Convert.ToDouble(
                driver.FindElement(By.ClassName("campaign-price"))
                .GetCssValue("font-size")
                .Replace("px", ""));

            //Old price font-size less then new price font-size
            Assert.IsTrue(newPriceFontsize > oldPriceFontSize);

            //Old price are grey               
            Assert.IsTrue(isColorGrey(driver.FindElement(By.ClassName("regular-price"))));
            //Old price is line-through	
            Assert.IsTrue(driver.FindElement(By.ClassName("regular-price")).GetCssValue("text-decoration").Contains("line-through"));

            //New price are red
            Assert.IsTrue(isColorRed(driver.FindElement(By.ClassName("campaign-price"))));
            //New price is strong==(bold, >700, etc)
            Assert.IsTrue(driver.FindElement(By.ClassName("campaign-price")).GetAttribute("tagName").Contains("STRONG"));
        }

        public bool isColorGrey(IWebElement el)
        {
            string[] elementColor = el.GetCssValue("color").
                Replace("rgba(", "").
                Replace(" ", "").
                Replace(")", "").
                Split(',');
            if ((elementColor[0] == elementColor[1]).Equals(elementColor[1] == elementColor[2]))
            {
                return true;
            }
            return false;
        }

        public bool isColorRed(IWebElement el)
        {
            string[] elementColor = el.GetCssValue("color").
                Replace("rgba(", "").
                Replace(" ", "").
                Replace(")", "").
                Split(',');
            if ((elementColor[1] == elementColor[2]).Equals(elementColor[1] == "0"))
            {
                return true;
            }
            return false;
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
