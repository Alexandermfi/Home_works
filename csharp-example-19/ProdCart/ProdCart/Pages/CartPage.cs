using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Collections.Generic;

namespace ProdCart.Pages
{
    internal class CartPage:Page
    {

        [FindsBy(How = How.Id, Using = "logotype-wrapper")]
        IWebElement Logotype;      

        [FindsBy(How = How.CssSelector, Using = "td.item")]
        IList<IWebElement> items;

        [FindsBy(How = How.XPath, Using = "//button[contains(.,'Remove')]")]
        IWebElement removeButton;

        internal CartPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
            this.driver = driver;
        }
        internal CartPage Open() {
            driver.Url = "http://litecart.stqa.ru/en/checkout";           
            return this;
        }


        internal CartPage ClearCart()
        {
            removeButton.Click();

            for (int i = items.Count; i > 0; i--)
            {
                if (i != items.Count)
                {
                   
                }
                else {
                    removeButton.Click();
                }
            }
        
            return this;
        }        
    }
}
