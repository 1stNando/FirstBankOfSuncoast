using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
namespace FirstBankOfSuncoast
{

    class Transaction
    {
        public int deposit { get; set; }

    }
    class TransactionDatabase///////////////////////////////////////////Database
    {
        private List<Transaction> Transactions { get; set; } = new List<Transaction>();

        private string FileName = "Transactions.csv";

        public void LoadTransactions()
        {
            if (File.Exists(FileName))
            {
                //Create Reader from Transactions.csv file
                var fileReader = new StreamReader(FileName);
                var csvReader = new CsvReader(fileReader, CultureInfo.InvariantCulture);
                //This replaces the empty list with the information contained inside the CSV file
                Transactions = csvReader.GetRecords<Transaction>().ToList();

                fileReader.Close();
            }
        }

        //Ability to write the Transaction list to the CSV file
        public void SavedTransactions()
        {
            var fileWriter = new StreamWriter(FileName);
            var csvWriter = new CsvWriter(fileWriter, CultureInfo.InvariantCulture);
            csvWriter.WriteRecords(Transactions);
            fileWriter.Close();
        }

        //Below we can write the behaviors we want this class to do.
        //CREATE Add Transaction.
        public void AddTransaction(Transaction newTransaction)
        {
            Transactions.Add(newTransaction);
        }

        //READ to get all Transaction made
        public List<Transaction> GetAllTransactions()
        {
            return Transactions;
        }


    }//////////////////////////////////////////////////////End of TransactionDatabase
    class Program
    {
        static void DisplayGreeting()
        {
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Welcome to your Personal Bank Account Manager");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine();
            Console.WriteLine();
        }

        static string PromptForString(string prompt)
        {
            Console.Write(prompt);
            var userInput = Console.ReadLine();

            return userInput;
        }

        static int PromptForInteger(string prompt)
        {
            Console.Write(prompt);
            int userInput;
            var isThisGoodInput = Int32.TryParse(Console.ReadLine(), out userInput);

            if (isThisGoodInput)
            {
                return userInput;
            }
            else
            {
                Console.WriteLine("Sorry, that isn't a valid input, I'm using 0 as your answer.");
                return 0;
            }
        }

        //Define the bank account type class
        public class BankAccount
        {
            //Add a member declaration.This account number will be assigned when the object is constructed. So we have unique account #.
            private static int accountNumberSeed = 1234567890;

            //Properties
            public string Number { get; }
            public string Owner { get; set; }
            public decimal Balance { get; }

            //Initialize the object
            public BankAccount(string name, decimal initialBalance)
            {
                Owner = name;
                Balance = initialBalance;
                //Adds a way for this constructor to assign the NEW account number.
                this.Number = accountNumberSeed.ToString();
                accountNumberSeed++;
            }

            //Methods
            public void MakeDeposit(decimal amount, DateTime date, string note)
            {

            }

            public void MakeWithdrawal(decimal amount, DateTime date, string note)
            {

            }

        }


        static void Main(string[] args)
        {
            //Initialize a constructor to create the new BankAccount
            var account = new BankAccount("Fernando", 500);
            Console.WriteLine($"Account {account.Number} was created for {account.Owner} with {account.Balance} initial balance. ");

            //Makes new database to save into
            var database = new TransactionDatabase();

            //We only need one instance at the beginning
            database.LoadTransactions();

            //Display the greeting
            DisplayGreeting();

            //Set the control for "should we keep going"
            var keepGoing = true;

            //Create the main WHILE loop
            while (keepGoing)
            {
                Console.WriteLine();
                Console.Write("What would you like to do?");

                //Lets create a SWITCH case to vary the user options
                var choice = Console.ReadLine().ToUpper();

                switch (choice)
                {
                    case "Q":
                        keepGoing = false;
                        break;

                        /////
                }
            }

            //One instance at the end of the program to SAVE Transactions
            database.SavedTransactions();


        }///End of Main.
    }
}

