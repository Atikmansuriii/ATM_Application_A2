using System;
using System.Collections.Generic;
using System.Linq;

public class Account
{
    // Properties of the Account class
    public int AccountNumber { get; set; }
    public double Balance { get; set; }
    public double InterestRate { get; set; }
    public string AccountHolderName { get; set; }
    private List<string> transactions;

    // Constructor to initialize an account
    public Account(int accountNumber, double initialBalance, double interestRate, string accountHolderName)
    {
        AccountNumber = accountNumber;
        Balance = initialBalance;
        InterestRate = interestRate;
        AccountHolderName = accountHolderName;
        transactions = new List<string>();
        RecordTransaction($"Account created with initial balance: ${initialBalance:F2}");
    }

    // Method to deposit money
    public void Deposit(double amount)
    {
        Balance += amount;
        RecordTransaction($"Deposited: ${amount:F2}");
    }

    // Method to withdraw money
    public void Withdraw(double amount)
    {
        if (amount <= Balance)
        {
            Balance -= amount;
            RecordTransaction($"Withdrew: ${amount:F2}");
        }
        else
        {
            Console.WriteLine("Insufficient funds.");
            RecordTransaction($"Failed withdrawal attempt: ${amount:F2}");
        }
    }

    // Method to record a transaction
    public void RecordTransaction(string transaction)
    {
        transactions.Add(transaction);
    }

    // Method to display all transactions
    public void DisplayTransactions()
    {
        Console.WriteLine($"Transactions for account {AccountNumber}:");
        foreach (var transaction in transactions)
        {
            Console.WriteLine(transaction);
        }
    }
}

public class Bank
{
    private List<Account> accounts;

    // Constructor to initialize the bank with 10 default accounts
    public Bank()
    {
        accounts = new List<Account>();
        for (int i = 0; i < 10; i++)
        {
            accounts.Add(new Account(100 + i, 100.0, 0.03, $"Default User {i + 1}"));
        }
    }

    // Method to add an account to the bank
    public void AddAccount(Account account)
    {
        accounts.Add(account);
    }

    // Method to retrieve an account by account number
    public Account RetrieveAccount(int accountNumber)
    {
        return accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
    }
}

public class AtmApplication
{
    private Bank bank;

    // Constructor to initialize the ATM application with a bank
    public AtmApplication()
    {
        bank = new Bank();
    }

    // Method to run the ATM application
    public void Run()
    {
        bool running = true;
        while (running)
        {
            Console.WriteLine("Welcome to the ATM Application!");
            Console.WriteLine("ATM Main Menu:");
            Console.WriteLine("1. Create Account");
            Console.WriteLine("2. Select Account");
            Console.WriteLine("3. Exit");

            switch (Console.ReadLine())
            {
                case "1":
                    CreateAccount();
                    break;
                case "2":
                    SelectAccount();
                    break;
                case "3":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
            Console.WriteLine(new string('-', 40));
        }
    }

    // Method to create a new account
    private void CreateAccount()
    {
        int accountNumber = -1;
        while (accountNumber < 100 || accountNumber > 1000)
        {
            Console.Write("Enter account number (between 100 and 1000): ");
            accountNumber = int.Parse(Console.ReadLine());
            if (accountNumber < 100 || accountNumber > 1000)
            {
                Console.WriteLine("Account number must be between 100 and 1000.");
            }
        }

        Console.Write("Enter initial balance: ");
        double initialBalance = double.Parse(Console.ReadLine());

        double interestRate = -1;
        while (interestRate < 0 || interestRate > 3)
        {
            Console.Write("Enter interest rate (between 0 and 3): ");
            interestRate = double.Parse(Console.ReadLine());
            if (interestRate < 0 || interestRate > 3)
            {
                Console.WriteLine("Interest rate must be between 0 and 3.");
            }
        }

        Console.Write("Enter account holder name: ");
        string accountHolderName = Console.ReadLine();

        var account = new Account(accountNumber, initialBalance, interestRate / 100, accountHolderName);
        bank.AddAccount(account);
        Console.WriteLine($"Account created successfully. Welcome, {accountHolderName}!");
        Console.WriteLine(new string('-', 40));
    }

    // Method to select an existing account and interact with it
    private void SelectAccount()
    {
        Console.Write("Enter account number: ");
        int accountNumber = int.Parse(Console.ReadLine());
        var account = bank.RetrieveAccount(accountNumber);

        if (account != null)
        {
            Console.WriteLine($"Welcome, {account.AccountHolderName}!");
            Console.WriteLine(new string('-', 40));

            bool accountRunning = true;
            while (accountRunning)
            {
                Console.WriteLine("Account Menu:");
                Console.WriteLine("1. Check Balance");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Display Transactions");
                Console.WriteLine("5. Exit Account");

                switch (Console.ReadLine())
                {
                    case "1":
                        // Display the balance
                        Console.WriteLine($"Balance: ${account.Balance:F2}");
                        break;
                    case "2":
                        // Deposit money
                        Console.Write("Enter deposit amount: ");
                        double depositAmount = double.Parse(Console.ReadLine());
                        account.Deposit(depositAmount);
                        Console.WriteLine("Deposit successful.");
                        break;
                    case "3":
                        // Withdraw money
                        Console.Write("Enter withdrawal amount: ");
                        double withdrawalAmount = double.Parse(Console.ReadLine());
                        account.Withdraw(withdrawalAmount);
                        Console.WriteLine("Withdrawal processed.");
                        break;
                    case "4":
                        // Display transactions
                        account.DisplayTransactions();
                        break;
                    case "5":
                        // Exit the account menu
                        accountRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
                Console.WriteLine(new string('-', 40));
            }
        }
        else
        {
            Console.WriteLine("Account not found.");
            Console.WriteLine(new string('-', 40));
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Initialize and run the ATM application
        AtmApplication atmApp = new AtmApplication();
        atmApp.Run();
    }
}
