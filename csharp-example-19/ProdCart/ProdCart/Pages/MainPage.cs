using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ProdCart.Pages
{
     class MainPage:Page
    {
        [FindsBy(How = How.XPath, Using = "//a[contains(.,'Checkout »')]")]
        IWebElement CheckOut;

        MainPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
            this.driver = driver;
        }
        //"//a[contains(.,'Checkout »')]"
        MainPage Open() {
            driver.Url = "http://litecart.stqa.ru/en/";
            return this;
        }

        MainPage ClickSomeProduct() {
            driver.FindElement(By.CssSelector("li.product")).Click();
            return this;
        }
    }
}

