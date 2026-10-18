using System;
using SimpleBankAccount.Models;

namespace SimpleBankAccount.Transactions
{
    public class TransactionService
    {
        public void Deposit(BankAccount account, decimal amount)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account),
                    "Account cannot be null.");
            }

            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount),
                    "Deposit amount must be greater than zero.");
            }

            if (amount > decimal.MaxValue - account.Balance)
            {
                throw new OverflowException(
                    "Deposit would exceed the maximum allowed balance.");
            }

            account.Deposit(amount);
        }

        public bool Withdraw(BankAccount account, decimal amount)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account),
                    "Account cannot be null.");
            }

            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount),
                    "Withdrawal amount must be greater than zero.");
            }

            if (amount > account.Balance)
            {
                return false;
            }

            return account.Withdraw(amount);
        }
    }
}