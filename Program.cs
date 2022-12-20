﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
namespace FirstBankOfSuncoast
{
    public class Transaction
    {
        public string TransactionType { get; set; }
        public int ChangeOfBalance { get; set; }
        public string TransactionHistory()
        {
            var getTransactionHistory = $"{TransactionType} for {ChangeOfBalance}";
            return getTransactionHistory;
        }

    }
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

        static void Main(string[] args)
        {
            var savingsAccountBalance = 0;
            var checkingAccountBalance = 0;
            var checkingTransactions = new List<Transaction>();

            if (File.Exists("checking.csv"))
            {
                var checkingFileReader = new StreamReader("checking.csv");
                var checkingCsvReader = new CsvReader(checkingFileReader, CultureInfo.InvariantCulture);

                checkingTransactions = checkingCsvReader.GetRecords<Transaction>().ToList();
            }

            foreach (var check in checkingTransactions)
            {
                if (check.TransactionType == "Deposit")
                {
                    checkingAccountBalance += check.ChangeOfBalance;
                }
                else
                {
                    checkingAccountBalance -= check.ChangeOfBalance;
                }

            }

            var savingsTransactions = new List<Transaction>();

            if (File.Exists("savings.csv"))
            {
                var savingsFileReader = new StreamReader("savings.csv");
                var savingsCsvReader = new CsvReader(savingsFileReader, CultureInfo.InvariantCulture);

                savingsTransactions = savingsCsvReader.GetRecords<Transaction>().ToList();
            }
            foreach (var save in savingsTransactions)
            {
                if (save.TransactionType == "Deposit")
                {
                    savingsAccountBalance += save.ChangeOfBalance;
                }
                else
                {
                    savingsAccountBalance -= save.ChangeOfBalance;
                }
            }

            //Makes new database we can manipulate and have it do actions. This variable references back to our database. Link in the chain.///////////////////
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
                Console.Write("What would you like to do?\n(D)eposit\n(W)ithdraw\n(V)iewTransactionHistory\n(B)alance\n(Q)uit");

                //Lets create a SWITCH case to vary the user options
                var choice = Console.ReadLine().ToUpper();

                switch (choice)
                {
                    case "Q":
                        keepGoing = false;
                        break;
                }
                switch (choice)
                {
                    case "W":
                        MakeWithdrawal
                        break;
                }

                switch (choice)
                {
                    case "D":
                        Console.WriteLine("Are you making a deposit into, (S)AVINGS or (C)HECKING");
                        var answer = Console.ReadLine().ToUpper();
                        if (answer == "C")
                        {
                            Console.WriteLine("State the amount of $dollars to deposit");
                            var depositAmount = int.Parse(Console.ReadLine());
                            var newTransaction = new Transaction
                            {
                                TransactionType = "Deposit",
                                ChangeOfBalance = depositAmount
                            };
                            var newCheckingAccountBalance = checkingAccountBalance + newTransaction.ChangeOfBalance;
                            checkingAccountBalance = newCheckingAccountBalance;
                            Console.WriteLine(checkingAccountBalance);
                            checkingTransactions.Add(newTransaction);
                        }
                        if (answer == "S")
                            Console.WriteLine("State the amount of $dollars to deposit");
                        var depositAmount = int.Parse(Console.ReadLine());
                        var newTransaction = new Transaction
                        {
                            TransactionType = "Deposit",
                            ChangeOfBalance = depositAmount
                        };
                        var newSavingsAccountBalance = savingsAccountBalance + newTransaction.ChangeOfBalance;
                        Console.WriteLine(savingsAccountBalance);
                        savingsTransactions.Add(newTransaction);
                        break;
                }

                switch (choice)
                {
                    case "W":
                        Console.WriteLine("Which account would you like to withdraw from, (C)hecking or (S)avings ");
                        var answer = Console.ReadLine().ToUpper();
                        if (answer == "C")
                        {
                            Console.WriteLine("State the amount in $dollars to withdraw ");
                            var withdrawAmount = int.Parse(Console.ReadLine());
                            var newTransaction = new Transaction
                            {
                                TransactionType = "Withdraw",
                                ChangeOfBalance = withdrawAmount
                            };
                            if (withdrawAmount < checkingAccountBalance)
                            {
                                var newCheckingAccountBalance = checkingAccountBalance - newTransaction.ChangeOfBalance;
                                checkingAccountBalance = newCheckingAccountBalance;
                                Console.WriteLine(checkingAccountBalance);
                                checkingTransactions.Add(newTransaction);
                            }
                            else
                            {
                                Console.WriteLine("You do not have sufficient funds for this! ");
                            }
                        }

                        break;
                }

                switch (choice)
                {
                    case "S":
                        Console.WriteLine(account.GetAccountHistory());
                        break;
                }
            }

            //One instance at the end of the program to SAVE Transactions
            database.SaveTransactions();


        }





    }
}

