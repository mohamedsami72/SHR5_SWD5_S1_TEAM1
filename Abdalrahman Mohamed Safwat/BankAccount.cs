using System;
namespace SimpleBankAccount.Models
{
    public class BankAccount
    {
        private string _accountNumber; public string AccountNumber { get => _accountNumber; private set { if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Account number cannot be empty."); _accountNumber = value.Trim(); } }
        public decimal Balance { get; private set; }

        public Customer Owner { get; private set; }

        public BankAccount(string accountNumber, Customer owner, decimal initialBalance = 0)
        {
            AccountNumber = accountNumber;

            if (owner == null)
                throw new ArgumentNullException(nameof(owner), "Account must have a valid customer owner.");
            Owner = owner;

            if (initialBalance < 0)
                throw new ArgumentException("Initial balance cannot be negative.");

            Balance = initialBalance;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Deposit amount must be greater than zero.");

            Balance += amount;
        }

        public bool Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Withdrawal amount must be greater than zero.");

            if (amount > Balance)
            {
                return false;
            }

            Balance -= amount;
            return true;
        }

        public override string ToString()
        {
            return $"Account No: {AccountNumber} | Owner: {Owner.Name} | Balance: {Balance:C}";
        }
    }
}