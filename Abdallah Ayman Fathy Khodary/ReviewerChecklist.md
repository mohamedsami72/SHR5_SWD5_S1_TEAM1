# Reviewer Checklist — SimpleBankAccount

## Code Review
- [ ] `Balance` has a private setter.
- [ ] Negative initial balance is rejected.
- [ ] Deposit and withdrawal reject zero and negative amounts.
- [ ] Withdrawal above balance is rejected.
- [ ] Namespaces and class names match across files.
- [ ] `ConsoleHelper` exists only once.
- [ ] Team has decided whether `BankAccount.Deposit` and `Withdraw` should be public.
- [ ] README structure matches the actual repository.

## Manual Testing
- [ ] Deposit a valid amount.
- [ ] Deposit zero.
- [ ] Deposit a negative amount.
- [ ] Withdraw a valid amount.
- [ ] Withdraw more than the balance.
- [ ] Enter letters in the menu.
- [ ] Enter letters at an amount prompt.
- [ ] Enter an invalid menu option such as 9.
- [ ] Exit with option 4.
- [ ] Test negative initial balance using a test harness/unit test.

## Documentation
- [ ] Update `TestReport.md` with actual observed results.
- [ ] Record the .NET SDK version and test environment.
- [ ] Report issues to the responsible team member.
