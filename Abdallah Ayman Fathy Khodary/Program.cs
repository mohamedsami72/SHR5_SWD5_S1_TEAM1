using System;
using SimpleBankAccount.Models;
using SimpleBankAccount.Transactions;
using SimpleBankAccount.UI;

namespace SimpleBankAccount.Models
{
    public class Customer
    {
        public int Id { get; private set; }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Customer name cannot be empty.");

                _name = value.Trim();
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || !value.Contains("@"))
                    throw new ArgumentException("Invalid email format.");

                _email = value.Trim();
            }
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Phone number cannot be empty.");

                _phoneNumber = value.Trim();
            }
        }

        public Customer(int id, string name, string email, string phoneNumber)
        {
            Id = id;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public override string ToString()
        {
            return $"[ID: {Id}] {Name} - {Email} ({PhoneNumber})";
        }
    }

    public class BankAccount
    {
        private string _accountNumber;

        public string AccountNumber
        {
            get => _accountNumber;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Account number cannot be empty.");

                _accountNumber = value.Trim();
            }
        }

        public decimal Balance { get; private set; }

        public Customer Owner { get; private set; }

        public BankAccount(
            string accountNumber,
            Customer owner,
            decimal initialBalance = 0)
        {
            AccountNumber = accountNumber;

            if (owner == null)
                throw new ArgumentNullException(
                    nameof(owner),
                    "Account must have a valid customer owner.");

            Owner = owner;

            if (initialBalance < 0)
                throw new ArgumentException(
                    "Initial balance cannot be negative.");

            Balance = initialBalance;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException(
                    "Deposit amount must be greater than zero.");

            Balance += amount;
        }

        public bool Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException(
                    "Withdrawal amount must be greater than zero.");

            if (amount > Balance)
                return false;

            Balance -= amount;
            return true;
        }

        public override string ToString()
        {
            return $"Account No: {AccountNumber} | Owner: {Owner.Name} | Balance: {Balance:C}";
        }
    }
}

namespace SimpleBankAccount.Transactions
{
    public class TransactionService
    {
        public void Deposit(BankAccount account, decimal amount)
        {
            if (account == null)
                throw new ArgumentNullException(
                    nameof(account), "Account cannot be null.");

            if (amount <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(amount),
                    "Deposit amount must be greater than zero.");

            if (amount > decimal.MaxValue - account.Balance)
                throw new OverflowException(
                    "Deposit would exceed the maximum allowed balance.");

            account.Deposit(amount);
        }

        public bool Withdraw(BankAccount account, decimal amount)
        {
            if (account == null)
                throw new ArgumentNullException(
                    nameof(account), "Account cannot be null.");

            if (amount <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(amount),
                    "Withdrawal amount must be greater than zero.");

            if (amount > account.Balance)
                return false;

            return account.Withdraw(amount);
        }
    }
}

namespace SimpleBankAccount.UI
{
    public static class ConsoleHelper
    {
        public static void ShowHeader()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("         ===================================================");
            Console.WriteLine("         ====            Simple Bank Account            ====");
            Console.WriteLine("         ===================================================");
            Console.WriteLine("         ====                By: Team1                  ====");
            Console.WriteLine("         ===================================================");
            Console.WriteLine("         ====            Welcome to Our Bank            ====");
            Console.WriteLine("         ===================================================");

            Console.ResetColor();
            Console.WriteLine();
        }

        public static string ReadString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                    return input.Trim();

                ShowError("Input cannot be empty.");
            }
        }

        public static int ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (int.TryParse(input, out int value))
                    return value;

                ShowError("Invalid input. Please enter a whole number.");
            }
        }

        public static decimal ReadDecimal(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (decimal.TryParse(input, out decimal value))
                    return value;

                ShowError("Invalid amount. Please enter a valid number.");
            }
        }

        public static void ShowSuccess(string message)
        {
            WriteColored($"[SUCCESS] {message}", ConsoleColor.Green);
        }

        public static void ShowError(string message)
        {
            WriteColored($"[ERROR] {message}", ConsoleColor.Red);
        }

        public static void ShowInfo(string message)
        {
            WriteColored(message, ConsoleColor.Cyan);
        }

        public static void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }

        private static void WriteColored(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }

    public class MainMenu
    {
        private readonly TransactionService _service;
        private readonly BankAccount _account;

        public MainMenu(TransactionService service, BankAccount account)
        {
            _service = service ??
                throw new ArgumentNullException(nameof(service));

            _account = account ??
                throw new ArgumentNullException(nameof(account));
        }

        public void Run()
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                ShowMenu();

                int userChoice =
                    ConsoleHelper.ReadInt("Choose Your Transaction : ");

                switch (userChoice)
                {
                    case 1:
                        CheckBalance();
                        break;

                    case 2:
                        Deposit();
                        break;

                    case 3:
                        Withdraw();
                        break;

                    case 4:
                        running = false;
                        ConsoleHelper.ShowInfo(
                            "Thank you for using our bank. Goodbye!");
                        continue;

                    default:
                        ConsoleHelper.ShowError(
                            "Invalid choice. Please choose a number from 1 to 4.");
                        break;
                }

                ConsoleHelper.Pause();
            }
        }

        private void ShowMenu()
        {
            ConsoleHelper.ShowHeader();

            Console.WriteLine($"Hello {_account.Owner.Name},");
            Console.WriteLine("*********************");
            Console.WriteLine(" 1- Check Balance");
            Console.WriteLine(" 2- Deposit");
            Console.WriteLine(" 3- Withdraw");
            Console.WriteLine(" 4- Exit");
            Console.WriteLine("*********************");
        }

        private void CheckBalance()
        {
            ConsoleHelper.ShowInfo(
                $"Account {_account.AccountNumber} balance: {_account.Balance:F2}");
        }

        private void Deposit()
        {
            decimal amount =
                ConsoleHelper.ReadDecimal("Enter amount to deposit : ");

            try
            {
                _service.Deposit(_account, amount);

                ConsoleHelper.ShowSuccess(
                    $"Deposited {amount:F2}. New balance: {_account.Balance:F2}");
            }
            catch (ArgumentOutOfRangeException)
            {
                ConsoleHelper.ShowError("Amount must be greater than zero.");
            }
            catch (OverflowException ex)
            {
                ConsoleHelper.ShowError(ex.Message);
            }
            catch (Exception ex)
            {
                ConsoleHelper.ShowError($"Deposit failed: {ex.Message}");
            }
        }

        private void Withdraw()
        {
            decimal amount =
                ConsoleHelper.ReadDecimal("Enter amount to withdraw : ");

            try
            {
                bool success = _service.Withdraw(_account, amount);

                if (success)
                {
                    ConsoleHelper.ShowSuccess(
                        $"Withdrew {amount:F2}. New balance: {_account.Balance:F2}");
                }
                else
                {
                    ConsoleHelper.ShowError(
                        $"Insufficient balance. Your balance is {_account.Balance:F2}.");
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                ConsoleHelper.ShowError("Amount must be greater than zero.");
            }
            catch (Exception ex)
            {
                ConsoleHelper.ShowError(
                    $"Withdrawal failed: {ex.Message}");
            }
        }
    }
}

namespace SimpleBankAccount
{
    public static class Program
    {
        public static void Main()
        {
            ConsoleHelper.ShowHeader();

            string name =
                ConsoleHelper.ReadString("Please Enter Your Name : ");

            var customer = new Customer(
                1,
                name,
                "customer@example.com",
                "01000000000");

            var account = new BankAccount("ACC-001", customer, 0m);

            var service = new TransactionService();

            var menu = new MainMenu(service, account);

            menu.Run();
        }
    }
}