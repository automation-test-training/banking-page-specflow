using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;

namespace banking_page_specflow.Pages
{
    class LoginModal : BasePage
    {
        [FindsBySequence]
        [FindsBy(How = How.CssSelector, Using = "ion-modal-view", Priority = 0)]
        [FindsBy(How = How.CssSelector, Using = "form", Priority = 1)]
        private IWebElement loginForm;

        public LoginModal(IWebDriver driver)
            : base(driver)
        {
            PageFactory.InitElements(this.driver, this);
        }

        public LoginModal WaitUntilVisible()
        {
            wait.Until(dr => loginForm.Displayed);
            return this;
        }

        public void LoginUser(String user)
        {
            new SelectElement(loginForm.FindElement(By.CssSelector("select"))).SelectByText(user);
            loginForm.Submit();
        }
    }
}
