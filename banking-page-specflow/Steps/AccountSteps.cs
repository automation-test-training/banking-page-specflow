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
            OpenHomePage();
        }

        private void OpenHomePage()
        {
            driver.Navigate().GoToUrl("http://10.211.55.2:8100/?ionicplatform=ios#/app/account");
            WaitForContentIsVisible();
        }

        [AfterScenario()]
        public void TearDown()
        {
            driver.Quit();
        }

        [Given(@"a user has (.*) accounts")]
        public void GivenAUserHasAccounts(int p0)
        {
            OpenSideMenu();

            ClickMenuItemByText("Login");

            WaitForModalIsVisible();
            var select = new SelectElement(driver.FindElement(By.CssSelector("ion-modal-view"))
                .FindElement(By.CssSelector("select")));
            select.SelectByText("heaton");
            driver.FindElement(By.CssSelector("ion-modal-view"))
                .FindElement(By.CssSelector("form")).Submit();
        }

        private void ClickMenuItemByText(String itemText)
        {
            var items = driver.FindElement(By.CssSelector("ion-side-menu"))
                .FindElement(By.CssSelector("ion-list"))
                .FindElements(By.CssSelector("ion-item"));

            var menu = from item in items where item.Text == itemText select item;
            menu.ElementAt(0).Click();
        }

        private void OpenSideMenu()
        {
            driver.FindElement(By.CssSelector("div.nav-bar-block[nav-bar=\"active\"]"))
                .FindElement(By.CssSelector("button.button.button-icon.button-clear.ion-navicon")).Click();

            WaitForSideMenuIsVisible();
        }

        [When(@"I refresh account")]
        public void WhenIRefreshAccount()
        {
            driver.Navigate().Refresh();
            WaitForContentIsVisible();
        }

        [Then(@"I should see accounts and balances:")]
        public void ThenIShouldSeeAccountsAndBalances(Table table)
        {

            var accountInfoList = driver.FindElement(By.CssSelector("ion-side-menu-content"))
                .FindElement(By.CssSelector("ion-content"))
                .FindElement(By.CssSelector("ion-list"))
                .FindElements(By.CssSelector("ion-item"));

            var account0Info = accountInfoList.ElementAt(0).FindElements(By.CssSelector("div"));
            Assert.AreEqual(account0Info.Count, 3);
            AssertAccountInfoShouldMatchRow(account0Info, table.Rows.ElementAt(0));


            var account1Info = accountInfoList.ElementAt(1).FindElements(By.CssSelector("div"));
            Assert.AreEqual(account1Info.Count, 2);
            AssertAccountInfoShouldMatchRow(account1Info, table.Rows.ElementAt(1));
        }

        private static void AssertAccountInfoShouldMatchRow(ReadOnlyCollection<IWebElement> account1Info, TableRow expectedAccount1Info)
        {
            for (int i = 0; i < account1Info.Count; i++)
            {
                Assert.AreEqual(account1Info.ElementAt(i).Text, expectedAccount1Info.ElementAt(i).Value);
            }
        }

        private void WaitForContentIsVisible()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("ion-side-menu-content")));
        }

        private void WaitForsideMenuIsVisible()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("ion-side-menu")));
        }

        private void WaitForModalIsVisible()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("ion-modal-view")));
        }

        private void WaitForSideMenuIsVisible()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("ion-side-menu")));
        }
    }
}
