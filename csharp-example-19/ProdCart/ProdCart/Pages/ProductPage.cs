using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;


namespace ProdCart.Pages
{
    class ProductPage: Page
    {
        
        [FindsBy(How = How.CssSelector, Using = "options[Size]")]
        SelectElement sizeSelector;

        [FindsBy(How = How.CssSelector, Using = "button[name =\"add_cart_product\"]")]
        IWebElement addToCartButton;

        [FindsBy(How = How.XPath, Using = "//span[@class='quantity']")]
        IWebElement quantity;

        [FindsBy(How = How.Id, Using = "logotype-wrapper")]
        IWebElement Logotype;


        ProductPage(IWebDriver driver): base(driver)
        {
            PageFactory.InitElements(driver, this);
            this.driver = driver;
        }
       

        ProductPage AddToCart(int countOfProduct)
        {
            if (sizeSelector != null) {
                sizeSelector.SelectByIndex(1);
            }
            for (int i = 0; i < countOfProduct; i++)
            {
                addToCartButton.Click();
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[@class='quantity'][contains(.," + (i + 1) + ")]")));
                Logotype.Click();
            }
            return this;
        }
    }
}
