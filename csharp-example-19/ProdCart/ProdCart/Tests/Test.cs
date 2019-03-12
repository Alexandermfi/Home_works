using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using ProdCart.Pages;

namespace csharp_example_10_1
{

    [TestFixture]
    public class Test

    {
        private ChromeDriver driver;
        private MainPage mainPage;
        private CartPage cartPage;
        private ProductPage prodPage;



        [SetUp]
        public void Start()
        {
                    
            driver = new ChromeDriver();
            mainPage = new MainPage(driver);
            cartPage = new CartPage(driver);
            prodPage = new ProductPage(driver);
        }

        [Test]
        public void TestMethod1()
        {   
            mainPage.Open().ClickSomeProduct();      
            prodPage.AddToCart(10);
            cartPage.Open().ClearCart();          
        }      

        [TearDown]
        public void Stop()
        {
            driver.Quit();
            driver = null;

        }
    }

}
