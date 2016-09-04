Feature: Show Accounts and Balance

  Scenario: Show multiple accounts
    Given a user has 2 accounts
    When I refresh account
    Then I should see accounts and balances:
      | account | cny balance  | usd balance |
      | 1001    | - cny 100000 | - usd 30000 |
      | 2001    | - cny 200000 |             |
