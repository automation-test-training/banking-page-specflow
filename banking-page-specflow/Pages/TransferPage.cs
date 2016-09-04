using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace banking_page_specflow.Pages
{
    class TransferPage : BasePage
    {
        [FindsBySequence]
        [FindsBy(How = How.CssSelector, Using = "ion-side-menu-content", Priority = 0)]
        [FindsBy(How = How.CssSelector, Using = "form", Priority = 1)]
        private IWebElement transferForm;

        [FindsBySequence]
        [FindsBy(How = How.CssSelector, Using = "div.nav-bar-block[nav-bar=\"active\"]", Priority = 0)]
        [FindsBy(How = How.CssSelector, Using = "button.button.button-icon.button-clear.ion-navicon", Priority = 1)]
        private IWebElement menuButton;

        public TransferPage(IWebDriver driver)
            : base(driver)
        {
            PageFactory.InitElements(this.driver, this);
        }

        public TransferPage WaitUntilVisible()
        {
            wait.Until(dr => transferForm.Displayed);
            return this;
        }

        public SideMenu OpenSideMenu()
        {
            menuButton.Click();
            return new SideMenu(driver);
        }

        public void TransferInSameCurrency(String amount, String from, String to)
        {
            transferForm.FindElement(By.CssSelector("input")).SendKeys(amount);
            new SelectElement(transferForm.FindElement(By.CssSelector("select[ng-model=\"from.accountNumber\"]"))).SelectByText(from);
            new SelectElement(transferForm.FindElement(By.CssSelector("select[ng-model=\"to.accountNumber\"]"))).SelectByText(to);
            transferForm.Submit();
        }
    }
}
