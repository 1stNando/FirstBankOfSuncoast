# FirstBankOfSuncoast

///////////////////////////////////////////////////////////////////
//Methods below: These methods will enforce the final two rules: the initial balance must be positive, and any withdrawal must not create a negative balance.
//Next, implement the MakeDeposit and MakeWithdrawal methods. These methods will enforce the final two rules: the initial balance must be positive, and any withdrawal must not create a negative balance

//Define the bank account type class
public class BankAccount
{
//Add a member declaration.This account number will be assigned when the object is constructed. So we have unique account #.
private static int accountNumberSeed = 1234567890;

            //Properties
            public string Number { get; }
            public string Owner { get; set; }

            //edit Balance property in order to calculate the Balance correctly by summing the values of ALL transactions. This will display the sum as a CURRENT balance.
            public decimal Balance
            {
                get
                {
                    decimal balance = 0;
                    foreach (var item in allTransactions)
                    {
                        balance += item.Amount;
                    }

                    return balance;
                }
            }

            //Initialize the object constructor
            public BankAccount(string name, decimal initialBalance)
            {
                //Adds a way for this constructor to assign the NEW account number.
                this.Number = accountNumberSeed.ToString();
                accountNumberSeed++;

                // The constructor should get a change so that it adds an initial transaction, rather than updating the balance directly. Since you already wrote the MakeDeposit method, call it from your constructor.
                Owner = name;
                MakeDeposit(initialBalance, DateTime.Now, "Initial balance");
            }

            //Lets add a List<> of Transaction objects
            private List<Transaction> allTransactions = new List<Transaction>();
        }


        /////////////////////////////
        //Write a method that creates a String for the transaction history
        public string GetAccountHistory()
        {
            var report = new System.Text.StringBuilder();

            decimal balance = 0;
            report.AppendLine("Date\t\tAmount\tBalance\tNote");
            foreach (var item in allTransactions)
            {
                balance += item.Amount;
                report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{balance}\t{item.Notes}");
            }
            return report.ToString();

            //The history uses the StringBuilder class to format a string that contains one line for each transaction.
            //One new character is \t. That inserts a tab to format the output.
        }

        ///////////////////////////////////
        public void MakeWithdrawal(decimal amount, DateTime date, string note)
        {
            var accountToUse = PromptForString("Which account (C)heckings or (S)avings?");
            if (accountToUse == "C")
            {
                var amount = PromptForInteger("How much?");
            }
            //The throw statement throws an exception. Execution of the current block ends, and control transfers to the first matching catch block found in the call stack. You'll add a catch block to test this code a little later on.
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawal must be (+) positive. ");
            }
            if (Balance - amount < 0)
            {
                throw new InvalidOperationException("Not sufficient funds for this withdrawal. ");
            }
            //Instance of a transaction. And recording of event to list of allTransactions.
            var withdrawal = new Transaction(-amount, date, note);
            allTransactions.Add(withdrawal);
        }

        //////////////////////////////////
        public void MakeDeposit(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be (+) positive. ");
            }
            var deposit = new Transaction(amount, date, note);
            allTransdactions.Add(deposit);
        }
