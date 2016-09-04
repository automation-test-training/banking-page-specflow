using System;
using TechTalk.SpecFlow;

namespace banking_page_specflow
{
    [Binding]
    public class ShowAccountsAndBalanceSteps
    {
        [Given(@"a user has (.*) accounts")]
        public void GivenAUserHasAccounts(int p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I refresh account")]
        public void WhenIRefreshAccount()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"I should see accounts and balances:")]
        public void ThenIShouldSeeAccountsAndBalances(Table table)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
