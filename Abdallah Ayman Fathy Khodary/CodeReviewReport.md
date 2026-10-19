# Code Review Report — SimpleBankAccount

## 1. Review Scope
Reviewed the C# source code shared for:
- `Customer`
- `BankAccount`
- `TransactionService`
- `ConsoleHelper`
- `MainMenu`
- `Program`

This is a source review based on the code provided in the conversation. The project was not compiled or executed in this review, so runtime results must be confirmed locally.

## 2. Summary
The submitted code demonstrates basic object-oriented structure, validation, and separation of responsibilities. Several required rules appear to be implemented. One design point needs discussion: `BankAccount.Deposit` and `BankAccount.Withdraw` are public, so callers can bypass `TransactionService` (although the methods still perform basic amount validation).

## 3. Findings

| ID | Area | Finding | Status | Recommendation |
|---|---|---|---|---|
| CR-01 | Encapsulation | `Balance` uses `public decimal Balance { get; private set; }`, so external code can read but cannot directly assign the balance. | Pass by source inspection | Keep the private setter. |
| CR-02 | Initial balance | Constructor rejects `initialBalance < 0` with `ArgumentException`. | Pass by source inspection | Add a runtime test for a negative initial balance. |
| CR-03 | Transaction validation | `TransactionService` rejects amounts `<= 0` for deposit and withdrawal. | Pass by source inspection | Test zero and negative amounts for both operations. |
| CR-04 | Insufficient funds | `Withdraw` returns `false` when the amount exceeds the balance; the UI reports insufficient balance. | Pass by source inspection | Test withdrawal greater than balance. |
| CR-05 | Public transaction methods | `BankAccount.Deposit` and `BankAccount.Withdraw` are public. This allows direct calls that bypass `TransactionService`. They still validate non-positive amounts, but the service-level checks and overflow guard can be bypassed. | Review / design decision | Confirm the team's requirement. If all transactions must go through the service, consider `internal` methods or another design approved by the team. |
| CR-06 | Overflow handling | `TransactionService.Deposit` checks whether the deposit would exceed `decimal.MaxValue` before calling `BankAccount.Deposit`. | Pass by source inspection | Keep the guard and test very large values if appropriate. |
| CR-07 | Console numeric input | `ReadInt` and `ReadDecimal` use `TryParse` and loop after invalid input. | Pass by source inspection | Test alphabetic input in the menu and amount prompts. |
| CR-08 | Menu validation | `MainMenu` handles unknown choices through the `default` case. | Pass by source inspection | Test a choice such as `9`. |
| CR-09 | Namespaces | The supplied classes consistently use `SimpleBankAccount.Models`, `SimpleBankAccount.Transactions`, `SimpleBankAccount.UI`, and `SimpleBankAccount`. | Pass by source inspection | Ensure file namespaces match the actual project structure. |
| CR-10 | Customer validation | Name and phone are checked for empty values; email is checked for empty values and the presence of `@`. | Pass by source inspection | Note that checking for `@` is basic validation, not full email-format validation. |
| CR-11 | Duplicate helper code | `ConsoleHelper` was supplied twice in the conversation. | Duplicate in supplied messages | Include the class only once in the project. |
| CR-12 | Exception types | `BankAccount.Deposit` and `Withdraw` throw `ArgumentException` for non-positive amounts, while `TransactionService` throws `ArgumentOutOfRangeException`. The UI catches the latter for service calls. | Pass in current call path | If methods are called directly, the UI-specific catch behavior may differ. Consider consistent exception conventions. |

## 4. Conclusion
The source code appears to cover the main requirements: balance encapsulation, non-negative initial balance, rejection of non-positive transaction amounts through the service, and basic console input validation. The main point to confirm with the team is whether transaction methods should remain public or be callable only through `TransactionService`.

This is not a claim that the application passed runtime tests. Compilation and manual tests should be completed and their actual outcomes recorded in `TestReport.md`.
