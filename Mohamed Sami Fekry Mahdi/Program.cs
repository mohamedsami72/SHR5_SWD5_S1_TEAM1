using System;
using SimpleBankAccount.Models;
using SimpleBankAccount.Transactions;
using SimpleBankAccount.UI;
namespace SimpleBankAccount
{
    public static class Program
    {
        public static void Main()
        {
            ConsoleHelper.ShowHeader();
            string name = ConsoleHelper.ReadString("Please Enter Your Name : ");
            var customer = new Customer(1, name, "customer@example.com", "01000000000");
            var account = new BankAccount("ACC-001", customer, 0m);
            var service = new TransactionService();
            var menu = new MainMenu(service, account);
            menu.Run();
        }
    }
}
