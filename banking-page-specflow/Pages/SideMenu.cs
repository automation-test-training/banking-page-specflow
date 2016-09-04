using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;

namespace banking_page_specflow.Pages
{
    class SideMenu : BasePage
    {

        [FindsBy(How = How.CssSelector, Using = "ion-side-menu")]
        private IWebElement sideMenu;

        public SideMenu(IWebDriver driver)
            : base(driver)
        {
            PageFactory.InitElements(this.driver, this);
        }

        public SideMenu WaitUntilVisible()
        {
            wait.Until(dr => sideMenu.Displayed);
            return this;
        }

        public void ClickMenuItemByText(String itemText)
        {
            var menuItems = sideMenu
                .FindElement(By.CssSelector("ion-list"))
                .FindElements(By.CssSelector("ion-item"));

            var filteredMenuItems = from menuItem in menuItems where menuItem.Text == itemText select menuItem;
            filteredMenuItems.ElementAt(0).Click();
        }
    }
}
