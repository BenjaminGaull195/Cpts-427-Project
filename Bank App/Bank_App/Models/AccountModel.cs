using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bank_App.Models
{
    public class BankAccount {
        public long AccountNum { get; set; }
        public string AccountName { get; set; }
        public string Type { get; set; }
        public double Balance { get; set; }
    }

    public class Transactions
    {
        public long AccountID { get; set; }
        public long TransactionID { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public string TransactionType { get; set; }
        public double Amount { get; set; }
    }

    public class AccountModel
    {
        public AccountModel()
        {
            accounts = new List<BankAccount>();
            Transactions = new List<Transactions>();
        }

        public List<BankAccount> accounts { get; set; }
        public List<Transactions> Transactions { get; set; }
    }
}