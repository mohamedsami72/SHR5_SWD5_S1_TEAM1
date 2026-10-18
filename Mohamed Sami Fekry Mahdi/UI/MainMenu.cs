using SimpleBankAccount.UI;
using System;
using SimpleBankAccount.Models;
using SimpleBankAccount.Transactions;
namespace SimpleBankAccount.UI
{
    public class MainMenu
    {
        private readonly TransactionService _service;
        private readonly BankAccount _account;
        public MainMenu(TransactionService service, BankAccount account)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _account = account ?? throw new ArgumentNullException(nameof(account));
        }
        public void Run()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                ShowMenu();
                int userChoice = ConsoleHelper.ReadInt("Choose Your Transaction : ");
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
                        ConsoleHelper.ShowInfo("Thank you for using our bank. Goodbye!");
                        continue;
                    default:
                        ConsoleHelper.ShowError("Invalid choice. Please choose a number from 1 to 4.");
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
            ConsoleHelper.ShowInfo($"Account {_account.AccountNumber} balance: {_account.Balance:F2}");
        }
        private void Deposit()
        {
            decimal amount = ConsoleHelper.ReadDecimal("Enter amount to deposit : ");
            try
            {
                _service.Deposit(_account, amount);
                ConsoleHelper.ShowSuccess($"Deposited {amount:F2}. New balance: {_account.Balance:F2}");
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
            decimal amount = ConsoleHelper.ReadDecimal("Enter amount to withdraw : ");
            try
            {
                bool success = _service.Withdraw(_account, amount);
                if (success)
                    ConsoleHelper.ShowSuccess($"Withdrew {amount:F2}. New balance: {_account.Balance:F2}");
                else
                    ConsoleHelper.ShowError($"Insufficient balance. Your balance is {_account.Balance:F2}.");
            }
            catch (ArgumentOutOfRangeException)
            {
                ConsoleHelper.ShowError("Amount must be greater than zero.");
            }
            catch (Exception ex)
            {
                ConsoleHelper.ShowError($"Withdrawal failed: {ex.Message}");
            }
        }
    }
}
