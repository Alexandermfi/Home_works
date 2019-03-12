using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ProdCart.Pages
{
     internal class MainPage:Page
    {
        [FindsBy(How = How.XPath, Using = "//a[contains(.,'Checkout »')]")]
        IWebElement CheckOut;

        [FindsBy(How = How.Id, Using = "logotype-wrapper")]
        IWebElement Logotype;

        internal MainPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
            this.driver = driver;
        }

        internal MainPage Open() {
            driver.Url = "http://litecart.stqa.ru/en/";
            return this;
        }

        internal MainPage ClickSomeProduct() {
            driver.FindElement(By.CssSelector("li.product")).Click();
            return this;
        }
    }
}

