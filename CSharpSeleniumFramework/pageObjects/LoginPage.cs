using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSeleniumFramework.pageObjects
{
    public class LoginPage
    {

        //driver.FindElement(By.Name("password")).SendKeys("learning");
        //driver.FindElement(By.CssSelector("#terms")).Click();
        //driver.FindElement(By.CssSelector("#signInBtn")).Click();

        IWebDriver driver;
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "username")]
        public IWebElement username { get; set; }

        [FindsBy(How=How.Name, Using = "password")]
        public IWebElement password { get; set; }

        [FindsBy(How = How.Id, Using = "terms")]
        public IWebElement checkbox { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#signInBtn")]
        public IWebElement signInBtn { get; set; }

        public ProductsPage validLogin(string user, string pass)
        {
            username.SendKeys(user);
            password.SendKeys(pass);
            checkbox.Click();
            signInBtn.Click();
            return new ProductsPage(driver);
        }


    }
}
