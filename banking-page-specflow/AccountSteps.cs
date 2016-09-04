using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using TechTalk.SpecFlow;
using System.Linq;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace banking_page_specflow
{
    [Binding]
    public class ShowAccountsAndBalanceSteps
    {
        private IWebDriver driver;

        [BeforeScenario()]
        public void Setup()
        {
            driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("http://10.211.55.2:8100/?ionicplatform=ios#/app/account");
        }

        [AfterScenario()]
        public void TearDown()
        {
            driver.Quit();
        }

        [Given(@"a user has (.*) accounts")]
        public void GivenAUserHasAccounts(int p0)
        {
            Thread.Sleep(3000);
            driver.FindElement(By.CssSelector("div.nav-bar-block[nav-bar=\"active\"]"))
                .FindElement(By.CssSelector("button.button.button-icon.button-clear.ion-navicon")).Click();

            Thread.Sleep(3000);


            var items = driver.FindElement(By.CssSelector("ion-side-menu"))
                .FindElement(By.CssSelector("ion-list"))
                .FindElements(By.CssSelector("ion-item"));


            var menu = from item in items where item.Text == "Login" select item;
            menu.ElementAt(0).Click();

            Thread.Sleep(3000);

            var select = new SelectElement(driver.FindElement(By.CssSelector("ion-modal-view"))
                .FindElement(By.CssSelector("select")));
            select.SelectByText("heaton");
            driver.FindElement(By.CssSelector("ion-modal-view"))
                .FindElement(By.CssSelector("form")).Submit();
        }

        [When(@"I refresh account")]
        public void WhenIRefreshAccount()
        {
            driver.Navigate().Refresh();
            Thread.Sleep(3000);
        }

        [Then(@"I should see accounts and balances:")]
        public void ThenIShouldSeeAccountsAndBalances(Table table)
        {
            var accounts = driver.FindElement(By.CssSelector("ion-side-menu-content"))
                .FindElement(By.CssSelector("ion-content"))
                .FindElement(By.CssSelector("ion-list"))
                .FindElements(By.CssSelector("ion-item"));

            var cols0 = accounts.ElementAt(0)
                .FindElements(By.CssSelector("div"));

            var row0 = table.Rows.ElementAt(0);
            Assert.AreEqual(cols0.Count, 3);
            Assert.AreEqual(cols0.ElementAt(0).Text, row0["account"]);
            Assert.AreEqual(cols0.ElementAt(1).Text, row0["cny balance"]);
            Assert.AreEqual(cols0.ElementAt(2).Text, row0["usd balance"]);


            var cols1 = accounts.ElementAt(1)
                .FindElements(By.CssSelector("div"));

            var row1 = table.Rows.ElementAt(1);
            Assert.AreEqual(cols1.Count, 2);
            Assert.AreEqual(cols1.ElementAt(0).Text, row1["account"]);
            Assert.AreEqual(cols1.ElementAt(1).Text, row1["cny balance"]);
        }
    }
}
