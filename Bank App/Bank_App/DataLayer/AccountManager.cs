using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bank_App.Models;
using Npgsql;

namespace Bank_App.DataLayer
{
    public static class AccountManager
    {
        public static void AddNewBankAccount(AccountFormModel model)
        {
            using (var connection = new NpgsqlConnection(buildConnection("authorizedUser", ">+T[qF^AYf;.n$%!6'eX~nxBu{M\"xQg2Kp5E5((aavgjS = 2y##KD$XZCm:v>qDew")))
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
            return String.Format("INSERT INTO userbankrelation() VALUES ('{0}', '(1)'", accountID, accountNum);
        }

        private static string FormatQuery()
        {
            //Select nextval(pg_get_serial_sequence('my_table', 'id')) as new_id;
            return "SELECT nextval(pg_get_serial_sequence('bankaccounts', 'accountnum')) AS nextID";
        }

    }
}