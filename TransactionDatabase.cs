using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.IO;
using CsvHelper;

// namespace FirstBankOfSuncoast
// {

//     public class TransactionDatabase
//     {
//         private List<Transaction> Transactions { get; set; } = new List<Transaction>();

//         private string FileName = "transactions.csv";

//         public void LoadTransactions()
//         {
//             if (File.Exists(FileName))
//             {
//                 //Create a READER for the Transactions.csv file
//                 var fileReader = new StreamReader(FileName);
//                 var csvReader = new CsvReader(fileReader, CultureInfo.InvariantCulture);
//                 //This replaces the empty list with the information contained inside the CSV file
//                 Transactions = csvReader.GetRecords<Transaction>().ToList();

//                 fileReader.Close();
//             }
//         }

//         //Ability to write the Transaction list to the CSV file
//         public void SaveTransactions()
//         {
//             var fileWriter = new StreamWriter(FileName);
//             var csvWriter = new CsvWriter(fileWriter, CultureInfo.InvariantCulture);
//             csvWriter.WriteRecords(Transactions);
//             fileWriter.Close();
//         }

//         //Create Add Transaction.
//         public void AddTransaction(Transaction newTransaction)
//         {
//             Transactions.Add(newTransaction);
//         }

//         //READ to get all Transaction made
//         public List<Transaction> GetAllTransactions()
//         {
//             return Transactions;
//         }


//     }//////////////////////////////////////////////////////End of TransactionDatabase

// }