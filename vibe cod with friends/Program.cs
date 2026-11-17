using System;
using System.Collections.Generic;
using System.Globalization;

namespace vibe_cod_with_friends
{
    internal class Program
    {
        private const decimal InitialBalanceUah = 150.00m;
        private const decimal UahToUsdRate = 41.00m;
        private const decimal UahToEurRate = 44.50m;

        private static decimal balanceUah;
        private static readonly List<Transaction> transactions =
            new List<Transaction>();

        private static void Main(string[] args)
        {
            balanceUah = InitialBalanceUah;

            AddTransaction(
                "Account opened",
                InitialBalanceUah,
                "UAH");

            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("=== Simple Bank System ===");
                Console.WriteLine("1. Show balance");
                Console.WriteLine("2. Deposit money");
                Console.WriteLine("3. Withdraw money");
                Console.WriteLine("4. Convert balance to USD and EUR");
                Console.WriteLine("5. Show transaction history");
                Console.WriteLine("0. Exit");
                Console.Write("\nChoose an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowBalance();
                        break;

                    case "2":
                        DepositMoney();
                        break;

                    case "3":
                        WithdrawMoney();
                        break;

                    case "4":
                        ConvertBalance();
                        break;

                    case "5":
                        ShowTransactionHistory();
                        break;

                    case "0":
                        isRunning = false;
                        continue;

                    default:
                        Console.WriteLine("\nInvalid option.");
                        Pause();
                        break;
                }
            }

            Console.WriteLine("Thank you for using the bank system.");
        }

        private static void ShowBalance()
        {
            Console.Clear();
            Console.WriteLine("=== Current Balance ===");
            Console.WriteLine("UAH: " + balanceUah.ToString("F2") + " ₴");
            Console.WriteLine("USD: " +
                (balanceUah / UahToUsdRate).ToString("F2") + " $");
            Console.WriteLine("EUR: " +
                (balanceUah / UahToEurRate).ToString("F2") + " €");

            Console.WriteLine("\nExchange rates:");
            Console.WriteLine("1 USD = " + UahToUsdRate.ToString("F2") + " UAH");
            Console.WriteLine("1 EUR = " + UahToEurRate.ToString("F2") + " UAH");

            Pause();
        }

        private static void DepositMoney()
        {
            Console.Clear();
            Console.WriteLine("=== Deposit Money ===");
            decimal amount = ReadPositiveAmount("Enter amount in UAH: ");

            balanceUah += amount;

            AddTransaction("Deposit", amount, "UAH");

            Console.WriteLine(
                "\nSuccessfully deposited " +
                amount.ToString("F2") + " UAH.");

            Pause();
        }

        private static void WithdrawMoney()
        {
            Console.Clear();
            Console.WriteLine("=== Withdraw Money ===");
            decimal amount = ReadPositiveAmount("Enter amount in UAH: ");

            if (amount > balanceUah)
            {
                Console.WriteLine("\nInsufficient funds.");
                Pause();
                return;
            }

            balanceUah -= amount;

            AddTransaction("Withdrawal", -amount, "UAH");

            Console.WriteLine(
                "\nSuccessfully withdrawn " +
                amount.ToString("F2") + " UAH.");

            Pause();
        }

        private static void ConvertBalance()
        {
            Console.Clear();
            Console.WriteLine("=== Currency Conversion ===");

            decimal usdAmount = balanceUah / UahToUsdRate;
            decimal eurAmount = balanceUah / UahToEurRate;

            Console.WriteLine("Current UAH balance: " +
                balanceUah.ToString("F2") + " UAH");
            Console.WriteLine("Converted to USD: " +
                usdAmount.ToString("F2") + " USD");
            Console.WriteLine("Converted to EUR: " +
                eurAmount.ToString("F2") + " EUR");

            AddTransaction(
                "Currency conversion preview",
                balanceUah,
                "UAH");

            Pause();
        }

        private static void ShowTransactionHistory()
        {
            Console.Clear();
            Console.WriteLine("=== Transaction History ===");

            if (transactions.Count == 0)
            {
                Console.WriteLine("No transactions found.");
            }
            else
            {
                foreach (Transaction transaction in transactions)
                {
                    Console.WriteLine(
                        transaction.Date.ToString("dd.MM.yyyy HH:mm:ss") +
                        " | " +
                        transaction.Description +
                        " | " +
                        transaction.Amount.ToString("F2") +
                        " " +
                        transaction.Currency);
                }
            }

            Pause();
        }

        private static void AddTransaction(
            string description,
            decimal amount,
            string currency)
        {
            transactions.Add(new Transaction
            {
                Date = DateTime.Now,
                Description = description,
                Amount = amount,
                Currency = currency
            });
        }

        private static decimal ReadPositiveAmount(string message)
        {
            decimal amount;

            while (true)
            {
                Console.Write(message);

                string input = Console.ReadLine();

                if (decimal.TryParse(
                    input,
                    NumberStyles.Number,
                    CultureInfo.InvariantCulture,
                    out amount) &&
                    amount > 0)
                {
                    return amount;
                }

                Console.WriteLine(
                    "Please enter a valid positive number.");
            }
        }

        private static void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    internal class Transaction // gowno
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}