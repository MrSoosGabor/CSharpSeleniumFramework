using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSeleniumFramework.pageObjects
{
    public class ProductsPage
    {
        IWebDriver driver;
        public By cardTitle { get; set; }
        public By addTocart { get; set; }
        public ProductsPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
            cardTitle = By.CssSelector(".card-title a");
            addTocart = By.CssSelector(".card-footer button");
        }

        [FindsBy(How = How.TagName, Using = "app-card")]
        public IList<IWebElement> cards { get; set; }

        [FindsBy(How = How.PartialLinkText, Using = "Checkout")]
        public IWebElement checkoutButton { get; set; }
    }
}
