using banking_page_specflow.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TechTalk.SpecFlow;

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
        }

        [AfterScenario()]
        public void TearDown()
        {
            driver.Quit();
        }

        [Given(@"a user has (.*) accounts")]
        public void GivenAUserHasAccounts(int p0)
        {
            var sideMenu = AccountPage.NavigateTo(driver).WaitUntilVisible()
                .OpenSideMenu().WaitUntilVisible();

            sideMenu.ClickMenuItemByText("Login");

            var loginDialog = new LoginModal(driver).WaitUntilVisible();
            loginDialog.LoginUser("heaton");
        }

        [When(@"I refresh account")]
        public void WhenIRefreshAccount()
        {
            driver.Navigate().Refresh();
        }

        [Then(@"I should see accounts and balances:")]
        public void ThenIShouldSeeAccountsAndBalances(Table table)
        {
            var accountPage = AccountPage.NavigateTo(driver).WaitUntilVisible();
            var accountInfoList = accountPage.GetAccountInfoList();

            var account0Info = accountInfoList.ElementAt(0);
            Assert.AreEqual(account0Info.Count(), 3);
            AccountPage.AssertAccountInfoShouldMatchRow(account0Info, table.Rows.ElementAt(0));

            var account1Info = accountInfoList.ElementAt(1);
            Assert.AreEqual(account1Info.Count(), 2);
            AccountPage.AssertAccountInfoShouldMatchRow(account1Info, table.Rows.ElementAt(1));
        }

    }
}
