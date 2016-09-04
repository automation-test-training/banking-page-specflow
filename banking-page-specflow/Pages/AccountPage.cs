using banking_page_specflow.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

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

        public IList<IWebElement> GetAccountInfoList()
        {
            return accountInfoList.FindElements(By.CssSelector("ion-item"));
        }
    }
}
