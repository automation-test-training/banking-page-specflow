using banking_page_specflow.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
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
            var accountPage = new AccountPage(driver).WaitUntilVisible();
            var accountInfoList = accountPage.GetAccountInfoList();

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
    }
}
