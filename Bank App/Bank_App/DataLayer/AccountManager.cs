using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bank_App.Models;
using Npgsql;
using NpgsqlTypes;

namespace Bank_App.DataLayer
{
    public static class AccountManager
    {
        public static void AddNewBankAccount(AccountFormModel model)
        {
            if (LoginService.IsAuthenticated)
            {
                using (var connection = new NpgsqlConnection(buildConnection("authorizedUser", "Rb&e@X@Em9wmZD52RY%*MLyw!cEf5h!e")))
                {
                    connection.Open();
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = FormatQuery();
                        long accountNum = 0;

                        try
                        {
                            var reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                accountNum = reader.GetInt64(0);
                            }

                            reader.Close();

                            cmd.CommandText = FormatAccountInsert(model, accountNum);
                            int altered = cmd.ExecuteNonQuery();
                            if (altered > 0)
                            {
                                cmd.CommandText = FormatAccountRelationInsert(accountNum, LoginService.CurrentUserID);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        catch (NpgsqlException ex)
                        {
                            Console.WriteLine("Database Error Occured: " + ex.Message.ToString());
                        }
                        finally
                        {
                            connection.Close();
                        }




                    }
                }
            }
            else
            {
                throw new Exception("User Not Authenticated");
            }
        }

        public static List<BankAccount> QueryAccounts(int accountNum)
        {
            if (LoginService.IsAuthenticated)
            {
                List<BankAccount> accounts = new List<BankAccount>();
                using (var connection = new NpgsqlConnection(buildConnection("authorizedUser", "Rb&e@X@Em9wmZD52RY%*MLyw!cEf5h!e")))
                {
                    connection.Open();
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = FormatAccountQuery(LoginService.CurrentUserID);
                        try
                        {
                            var reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                accounts.Add(new BankAccount() {AccountNum = reader.GetInt64(0), AccountName = reader.GetString(1), Type = reader.GetString(2), Balance = reader.GetDouble(3) });

                            }
                        }
                        catch (NpgsqlException ex)
                        {
                            Console.WriteLine("Database Error Occured: " + ex.Message.ToString());
                        }
                        finally
                        {
                            connection.Close();
                        
                        }
                        return accounts;
                    }
                }

            }
            else
            {
                throw new Exception("User Not Authenticated");
            }
        }


        public static List<Transactions> QueryTransactions(int accountNum)
        {
            if (LoginService.IsAuthenticated)
            {
                List<Transactions> transactions = new List<Transactions>();
                using (var connection = new NpgsqlConnection(buildConnection("authorizedUser", "Rb&e@X@Em9wmZD52RY%*MLyw!cEf5h!e")))
                {
                    connection.Open();
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = FormatTransactionQuery(LoginService.CurrentUserID);
                        try
                        {
                            var reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                Transactions temp = new Transactions();

                                temp.AccountID = reader.GetInt64(0);
                                temp.TransactionID = reader.GetInt64(1);
                                temp.TransactionDateTime = reader.GetTimeStamp(2).ToDateTime();
                                temp.TransactionType = reader.GetString(3);
                                temp.Amount = reader.GetDouble(4);
                                

                                transactions.Add(temp);

                            }
                        }
                        catch (NpgsqlException ex)
                        {
                            Console.WriteLine("Database Error Occured: " + ex.Message.ToString());
                        }
                        finally
                        {
                            connection.Close();
                        }
                        return transactions;
                    }
                }
            }
            else
            {
                throw new Exception("User Not Authenticated");
            }
        }
        

        private static string buildConnection(string user = "", string pass = "")
        {
            return String.Format("Host = localhost; Username = {0}; Database = Bank_App; Password = {1}", user, pass);
        }

        private static string FormatAccountInsert(AccountFormModel model, long accountNum)
        {
            return String.Format("INSERT INTO bankaccounts(accountnum, accountname, accounttype, amount) VALUES ('{0}', '{1}', '{2}', '{3}');",accountNum, model.name, model.type, 0.0);
        }

        private static string FormatAccountRelationInsert(long accountNum, int accountID)
        {
            return String.Format("INSERT INTO userbankrelation(useraccountID, bankaccountID) VALUES ('{0}', '{1}')", accountID, accountNum);
        }

        private static string FormatQuery()
        {
            //Select nextval(pg_get_serial_sequence('my_table', 'id')) as new_id;
            return "SELECT nextval(pg_get_serial_sequence('bankaccounts', 'accountnum')) AS nextID";
        }

        private static string FormatAccountQuery(int accountID)
        {
            return String.Format("SELECT bankaccounts.accountnum, bankaccounts.accountname, bankaccounts.accounttype, bankaccounts.amount FROM userbankrelation INNER JOIN bankaccounts ON userbankrelation.bankaccountid = bankaccounts.accountnum WHERE userbankrelation.useraccountid = '{0}';", accountID);
        }

        private static string FormatTransactionQuery(int accountID)
        {
            return String.Format("SELECT transactions.accountid, transactions.transactionid, transactions.transactiondate, transactions.transactiontype, transactions.transactionamount FROM userbankrelation INNER JOIN bankaccounts ON userbankrelation.bankaccountid = bankaccounts.accountnum INNER JOIN transactions ON bankaccounts.accountnum = transactions.accountid WHERE userbankrelation.useraccountid = '{0}';", accountID);
        }

    }
}