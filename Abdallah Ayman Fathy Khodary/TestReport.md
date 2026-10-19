# Test Report — SimpleBankAccount

## 1. Test Information
- Application: SimpleBankAccount
- Test type: Manual console tests and source-level checks
- Test environment: Fill in the OS, .NET SDK version, and IDE used.
- Execution status: **Not executed in this report.** The expected outcomes are listed below; fill in actual results after running the program.

## 2. Test Cases

| ID | Test | Steps / Input | Expected Result | Actual Result | Status |
|---|---|---|---|---|---|
| TC-01 | Valid deposit | Start with balance `0`; choose Deposit; enter `100` | Deposit succeeds; balance becomes `100` | Not run | Not run |
| TC-02 | Deposit zero | Choose Deposit; enter `0` | Operation rejected; balance unchanged | Not run | Not run |
| TC-03 | Deposit negative amount | Choose Deposit; enter `-50` | Operation rejected; balance unchanged | Not run | Not run |
| TC-04 | Valid withdrawal | Have balance `100`; choose Withdraw; enter `30` | Withdrawal succeeds; balance becomes `70` | Not run | Not run |
| TC-05 | Withdrawal greater than balance | Have balance `100`; choose Withdraw; enter `150` | Withdrawal rejected with insufficient-balance message; balance remains `100` | Not run | Not run |
| TC-06 | Initial balance is negative | Create `BankAccount` with `initialBalance: -10` in a test harness | Constructor throws `ArgumentException` | Not run | Not run |
| TC-07 | Letters in menu choice | At the menu, enter `abc` | Error message appears; program asks for the choice again | Not run | Not run |
| TC-08 | Letters in amount | At Deposit or Withdraw prompt, enter `abc` | Error message appears; program asks for amount again | Not run | Not run |
| TC-09 | Invalid menu option | At the menu, enter `9` | Invalid-choice message appears; menu returns after pause | Not run | Not run |
| TC-10 | Exit | At the menu, enter `4` | Goodbye message appears and application exits the menu loop | Not run | Not run |
| TC-11 | Empty customer name | At startup, press Enter without typing a name | Error appears and name is requested again | Not run | Not run |
| TC-12 | Empty account number | Construct account with an empty/whitespace account number in a test harness | Constructor throws `ArgumentException` | Not run | Not run |

## 3. How to Execute the Tests
1. Open the project folder in a terminal.
2. Run `dotnet run`.
3. Record the balance before and after each deposit/withdrawal.
4. For TC-06 and TC-12, use a small test harness or unit tests because these constructor cases are not exposed by the normal menu.
5. Replace `Not run` with the observed output and set Status to `Pass` or `Fail`. Do not mark a test as passed until it has actually been executed.

## 4. Test Result Summary
- Total test cases: 12
- Passed: 0 confirmed in this report
- Failed: 0 confirmed in this report
- Not run / pending execution: 12

These counts indicate that no runtime test results were provided or executed while preparing this document. Update the summary after testing.
