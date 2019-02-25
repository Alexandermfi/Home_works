using System;
using System.IO;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace csharp_example_1
{

    [TestFixture]
    public class EnterNewProduct
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

            driver.Url = "http://localhost/litecart/admin/.";
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Name("login")));
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            driver.FindElement(By.XPath("//span[contains(.,'Catalog')]")).Click();

            driver.FindElement(By.XPath("//a[contains(text(),' Add New Product')]")).Click();
            driver.FindElement(By.Name("name[en]")).SendKeys("Super Duck");
            driver.FindElement(By.Name("code")).SendKeys("10001");
            new Actions(driver).MoveToElement(driver.FindElement(By.XPath("//*[@data-name='Rubber Ducks']"))).Click().Perform();
            new Actions(driver).MoveToElement(driver.FindElement(By.XPath("//td[contains(text(),'Unisex')]/preceding-sibling::td[1]"))).Click().Perform();
            driver.FindElement(By.Name("quantity")).SendKeys("50");
            new SelectElement(driver.FindElement(By.Name("sold_out_status_id"))).SelectByIndex(1);
            //File upload
            string relativePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\resource\\rubber_superduck.jpg");
            string path = Path.GetFullPath(relativePath);
            driver.FindElement(By.Name("new_images[]")).SendKeys(path);

            SetDatepicker(driver, "input[name='date_valid_from']", "25/02/2018");
            SetDatepicker(driver, "input[name='date_valid_to']", "26/02/2018");

            //Information Tab
            driver.FindElement(By.XPath("//a[contains(.,'Information')]")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Name("manufacturer_id")));
            new SelectElement(driver.FindElement(By.Name("manufacturer_id"))).SelectByIndex(1);
            //supplier_id select is Empty
            //new SelectElement(driver.FindElement(By.Name("supplier_id"))).SelectByIndex(1);

            driver.FindElement(By.Name("keywords")).SendKeys("duck, super, supreduck");
            driver.FindElement(By.Name("short_description[en]")).SendKeys("It's a Bird... It's a Plane... It's SuperDuck");

            string descriptionText = "Sed ut perspiciatis unde omnis iste natus error " +
                "sit voluptatem accusantium doloremque laudantium, totam rem " +
                "aperiam, eaque ipsa quae ab illo inventore veritatis et " +
                "quasi architecto beatae vitae dicta sunt explicabo.Nemo enim" +
                " ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit," +
                " sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt." +
                " Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, " +
                "sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem. " +
                "Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid " +
                "ex ea commodi consequatur? Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil" +
                " molestiae consequatur, vel illum qui dolorem eum fugiat quo voluptas nulla pariatur ? ";

            driver.FindElement(By.CssSelector("div.trumbowyg-editor")).SendKeys(descriptionText);

            driver.FindElement(By.Name("head_title[en]")).SendKeys("SuperDuck");
            driver.FindElement(By.Name("meta_description[en]")).SendKeys("SuperDuck");


            //Prices Tab
            driver.FindElement(By.XPath("//a[contains(.,'Prices')]")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Name("purchase_price")));
            driver.FindElement(By.Name("purchase_price")).Clear();
            driver.FindElement(By.Name("purchase_price")).SendKeys("30");
            new SelectElement(driver.FindElement(By.Name("purchase_price_currency_code"))).SelectByIndex(1);
            //Tax class is empty
            //new SelectElement(driver.FindElement(By.Name("tax_class_id"))).SelectByIndex(1);

            driver.FindElement(By.Name("prices[USD]")).SendKeys("30");
            driver.FindElement(By.Name("prices[EUR]")).SendKeys("28");

            driver.FindElement(By.Name("save")).Click();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//h1[contains(.,'Catalog')]")));

            Assert.IsTrue(IsElementPresent(driver, By.XPath("//a[contains(.,'Super Duck')]")));
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

        public void SetDatepicker(IWebDriver driver, string cssSelector, string date)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(30)).Until<bool>(
                d => driver.FindElement(By.CssSelector(cssSelector)).Displayed);
            driver.FindElement(By.CssSelector(cssSelector)).SendKeys(date);
            driver.FindElement(By.CssSelector("body")).Click();
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
