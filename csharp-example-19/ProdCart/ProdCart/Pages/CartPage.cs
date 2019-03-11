using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ProdCart.Pages
{
    class CartPAge:Page
    {


        [FindsBy(How = How.XPath, Using = "//button[contains(.,'Remove')]")]
        IWebElement removeButton;      


        CartPAge(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
            this.driver = driver;
        }


        CartPAge RemoveFromCart()
        {
            removeButton.Click();
            return this;
        }
    }
}
