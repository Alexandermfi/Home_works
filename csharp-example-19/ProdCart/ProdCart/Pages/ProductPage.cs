using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;


namespace ProdCart.Pages
{
    internal class ProductPage: Page
    {
        
        [FindsBy(How = How.Name, Using = "options[Size]")]
        IWebElement sizeSelector;

        [FindsBy(How = How.CssSelector, Using = "button[name =\"add_cart_product\"]")]
        IWebElement addToCartButton;

        [FindsBy(How = How.XPath, Using = "//span[@class='quantity']")]
        IWebElement quantity;

        [FindsBy(How = How.Id, Using = "logotype-wrapper")]
        IWebElement Logotype;

        private MainPage mainPage;

        internal ProductPage(IWebDriver driver): base(driver)
        {
            PageFactory.InitElements(driver, this);
            mainPage = new MainPage(driver);
            this.driver = driver;
        }


        internal ProductPage AddToCart(int countOfProduct)
        {
            
            for (int i = 0; i < countOfProduct; i++)
            {
                try
                    {
                        new SelectElement(sizeSelector).SelectByIndex(1);
                    }                  
                        catch (NoSuchElementException ex)
                    {
                        
                    }
                
                addToCartButton.Click();
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[@class='quantity'][contains(.," + (i + 1) + ")]")));
                Logotype.Click();
                mainPage.ClickSomeProduct();
            }
            return this;
        }
       
    }
}
