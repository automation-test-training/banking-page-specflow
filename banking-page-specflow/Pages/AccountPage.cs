using banking_page_specflow.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace banking_page_specflow
{
    class AccountPage : BasePage
    {
        [FindsBySequence]
        [FindsBy(How = How.CssSelector, Using = "ion-side-menu-content", Priority = 0)]
        [FindsBy(How = How.CssSelector, Using = "ion-content", Priority = 1)]
        [FindsBy(How = How.CssSelector, Using = "ion-list", Priority = 2)]
        private IWebElement accountInfoList;

        [FindsBySequence]
        [FindsBy(How = How.CssSelector, Using = "div.nav-bar-block[nav-bar=\"active\"]", Priority = 0)]
        [FindsBy(How = How.CssSelector, Using = "button.button.button-icon.button-clear.ion-navicon", Priority = 1)]
        private IWebElement menuButton;

        public AccountPage(IWebDriver driver)
            : base(driver)
        {
            PageFactory.InitElements(this.driver, this);
        }

        public AccountPage WaitUntilVisible()
        {
            wait.Until(dr => accountInfoList.Displayed);
            return this;
        }

        public static AccountPage NavigateTo(IWebDriver webDriver)
        {
            webDriver.Navigate().GoToUrl("http://10.211.55.2:8100/?ionicplatform=ios#/app/account");
            return new AccountPage(webDriver);
        }

        public SideMenu OpenSideMenu()
        {
            menuButton.Click();
            return new SideMenu(driver);
        }

        public IEnumerable<IEnumerable<String>> GetAccountInfoList()
        {
            return from accountElement in accountInfoList.FindElements(By.CssSelector("ion-item"))
                   select GetBalanceList(accountElement);
        }

        private static IEnumerable<String> GetBalanceList(IWebElement accountElement)
        {
            return from balance in accountElement.FindElements(By.CssSelector("div"))
                   select balance.Text;
        }

        public IEnumerable<IEnumerable<String>> GetAccountInfoOfCurrencyList(String currency)
        {
            return from accountElement in accountInfoList.FindElements(By.CssSelector("ion-item"))
                   select GetBalanceList(accountElement, currency);
        }

        private static IEnumerable<String> GetBalanceList(IWebElement accountElement,String currency)
        {
            return from balance in accountElement.FindElements(By.CssSelector("div"))
                   where !balance.Text.StartsWith("-") || balance.Text.Contains("curreny")  
                   select balance.Text;
        }

        public static void AssertAccountInfoShouldMatchRow(IEnumerable<String> account1Info, TableRow expectedAccount1Info)
        {
            for (int i = 0; i < account1Info.Count(); i++)
            {
                Assert.AreEqual(account1Info.ElementAt(i), expectedAccount1Info.ElementAt(i).Value);
            }
        }
    }
}
