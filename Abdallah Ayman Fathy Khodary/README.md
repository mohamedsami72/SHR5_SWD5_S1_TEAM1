# Simple Bank Account

## Project Overview
Simple Bank Account is a C# console application that demonstrates basic banking operations for one customer account. The user can view the balance, deposit money, withdraw money, and exit the application through a text menu.

## Features
- Display account balance.
- Deposit funds.
- Withdraw funds when sufficient balance is available.
- Reject zero or negative transaction amounts through `TransactionService`.
- Prevent creation of an account with a negative initial balance.
- Validate numeric console input and re-prompt when the input is invalid.
- Display colored success, error, and information messages.

## Technologies
- C#
- .NET Console Application
- Object-Oriented Programming (OOP)

## Source Components
The original multi-file structure is expected to be similar to:

```text
SimpleBankAccount/
├── Models/
│   ├── Customer.cs
│   └── BankAccount.cs
├── Transactions/
│   └── TransactionService.cs
├── UI/
│   ├── ConsoleHelper.cs
│   └── MainMenu.cs
├── Program.cs
└── README.md
```

If the team combines all classes into a single `Program.cs`, update this section to reflect the actual files in the submitted project.

## How to Run
1. Install the .NET SDK.
2. Open a terminal in the directory containing the `.csproj` file.
3. Run:

```bash
dotnet run
```

## Menu
1. Check Balance
2. Deposit
3. Withdraw
4. Exit

## Validation Notes
- The account balance has a private setter.
- A negative initial balance is rejected.
- The transaction service rejects zero and negative amounts.
- A withdrawal larger than the available balance returns `false` and the UI reports insufficient balance.
- The console helper re-prompts after invalid integer or decimal input.

## Review and Testing
See:
- `CodeReviewReport.md` for source-level findings.
- `TestReport.md` for test cases and places to record actual outcomes.

**Important:** Do not claim that runtime tests passed until they have been executed and their actual outcomes recorded.
