using CSharpSeleniumFramework.pageObjects;
using CSharpSeleniumFramework.utilities;
using OpenQA.Selenium;

namespace CSharpSeleniumFramework
{
    public class E2ETest: Base
    {
        [Test]
        [TestCaseSource("testDatas")]
        public void EndToEndFlow(string username, string password)
        {
            string[] expectedProducts = { "iphone X", "Blackberry" };
            LoginPage loginPage = new LoginPage(driver);
            ProductsPage productsPage = loginPage.validLogin(username, password);

            foreach (IWebElement product in productsPage.cards)
            {
                if (expectedProducts.Contains(product.FindElement(By.CssSelector(".card-title a")).Text))
                {
                    product.FindElement(By.CssSelector(".card-footer button")).Click();
                }
                TestContext.Progress.WriteLine(product.FindElement(productsPage.cardTitle).Text);
            }
            productsPage.checkoutButton.Click();

            CheckOutPage checkOutPage = new CheckOutPage(driver);

            // Megjelenik-e a két termék a checkout oldalon?
            foreach (IWebElement prod in checkOutPage.checkoutCards)
            {
                TestContext.Progress.WriteLine(prod.Text);
            }

        }

        [Test, Category("Smoke")]
        public void LocatorIdentification()
        {
            driver.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");
            driver.FindElement(By.Id("password")).SendKeys("learning");
        }


        public static TestCaseData[] testDatas =
        {
            new TestCaseData(reader.extractData("username"), reader.extractData("password")),
            new TestCaseData(reader.extractData("username_wrong"), reader.extractData("password_wrong"))
        };
    }
}