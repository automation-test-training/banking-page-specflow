﻿using banking_page_specflow.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using TechTalk.SpecFlow;
namespace banking_page_specflow.Steps
{
    [Binding]
    public class TransferSteps
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
            driver.Close();
        }

        [Given(@"I have accounts of ""(.*)"":")]
        public void GivenIHaveAccounts(String currency, Table table)
        {
            var accountPage = AccountPage.NavigateTo(driver).WaitUntilVisible();
            var accountInfoList = accountPage.GetAccountInfoOfCurrencyList(currency);

            var account0Info = accountInfoList.ElementAt(0);
            Assert.AreEqual(account0Info.Count(), 1);
            AccountPage.AssertAccountInfoShouldMatchRow(account0Info, table.Rows.ElementAt(0));

            var account1Info = accountInfoList.ElementAt(1);
            Assert.AreEqual(account0Info.Count(), 1);
            AccountPage.AssertAccountInfoShouldMatchRow(account1Info, table.Rows.ElementAt(1));
        }

        [When(@"I transfer (.*) cny from ""(.*)"" to ""(.*)""")]
        public void WhenITransferCnyFromTo(String amount, String from, String to)
        {
            var sideMenu = new AccountPage(driver).WaitUntilVisible().OpenSideMenu();
            sideMenu.ClickMenuItemByText("Transfer");

            new TransferPage(driver).WaitUntilVisible().TransferInSameCurrency(amount, from, to);
        }

        [Then(@"my ""(.*)"" balance should be:")]
        public void ThenMyBalanceShouldBe(String currency, Table table)
        {
            var sideMenu  = new TransferPage(driver).WaitUntilVisible()
                .OpenSideMenu().WaitUntilVisible();
            sideMenu.ClickMenuItemByText("Account");

            var accountPage = new AccountPage(driver).WaitUntilVisible();
            var accountInfoList = accountPage.GetAccountInfoOfCurrencyList(currency);

            var account0Info = accountInfoList.ElementAt(0);
            Assert.AreEqual(account0Info.Count(), 1);
            AccountPage.AssertAccountInfoShouldMatchRow(account0Info, table.Rows.ElementAt(0));

            var account1Info = accountInfoList.ElementAt(1);
            Assert.AreEqual(account0Info.Count(), 1);
            AccountPage.AssertAccountInfoShouldMatchRow(account1Info, table.Rows.ElementAt(1));
        }
    }
}
