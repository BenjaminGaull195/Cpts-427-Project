﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using NpgsqlTypes;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Bank_App
{
    public class StoredUser
    {
        public int accountID { get; set; }
        public string username { get; set; }
        public string passwordhash { get; set; }
    }

    public class LoginService
    {   
        LoginService()
        {
            
        }

        public static int CurrentUserID { get; private set; }
        public static string CurrentUserName { get; private set; }
        public static bool HasUserName { get; private set; }
        public static bool IsAuthenticated { get; private set; }


        public bool RegisterUser(string username, string email, string password)
        {
            try
            {
                //compute password hash
                string hashed = ComputeHash(password);

                //store user to database
                using (NpgsqlConnection connection = new NpgsqlConnection(buildConnection("LoginService", "H-bjMbMM.^Z>f#$Tqq+WddB6x+-<A-WG")))
                {
                    connection.Open();

                    using (NpgsqlCommand cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = FormatInsert(username, email, hashed);

                        //if error return false, else return true
                        int affected = cmd.ExecuteNonQuery();
                        if (affected == 1)
                        {
                            return true;
                        }
                    }
                    connection.Close();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("User Registration Error Occured: " + ex.Message.ToString());
                return false;
            }
            return false;
        }

        public bool SignIn(string login, string password)
        {
            //lookup user by username/email (login) and retrieve password hash and salt
            StoredUser user = new StoredUser();
            using (NpgsqlConnection connection = new NpgsqlConnection(buildConnection("LoginService", "H-bjMbMM.^Z>f#$Tqq+WddB6x+-<A-WG"))) {
                connection.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT accountid, username, passwordhash FROM UserAccounts WHERE email = :emailaddr OR username = :user";
                    cmd.Parameters.Add(new NpgsqlParameter(":emailaddr", NpgsqlDbType.Varchar) { Value = login });
                    cmd.Parameters.Add(new NpgsqlParameter(":user", NpgsqlDbType.Varchar) { Value = login });
                    try
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            user.accountID = reader.GetInt32(0);
                            user.username = reader.GetString(1);
                            user.passwordhash = reader.GetString(2);
                        }
                    }
                    catch (NpgsqlException ex)
                    {
                        Console.WriteLine("Database Exception Occured: " + ex.Message.ToString());
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            //compute hash and compare hash to stored hash, Set IsAuthenticated
            IsAuthenticated = CompareHash(password, user.passwordhash);
            if (IsAuthenticated)
            {
                CurrentUserID = user.accountID;
                CurrentUserName = user.username;
                if (user.username == "")
                {
                    HasUserName = false;
                }
                HasUserName = true;
                //generate cookie 


                return true;
            }
            //
            return false;

        }

        public void SignOut()
        {
            CurrentUserName = "";
            CurrentUserID = -1;
            HasUserName = false;
            IsAuthenticated = false;

            //clear cookie
        }

        public void ResetPassword(string username, string currentpass, string newpass)
        {

        }




        private string buildConnection(string user = "", string pass = "")
        {
            return String.Format("Host = localhost; Username = {0}; Database = Bank_App; Password = {1}", user, pass);
        }

        private string FormatInsert(string username, string email, string password)
        {
            return String.Format("INSERT INTO UserAccounts() VALUES ('{0}', '{1}', '{2}')", username, email, password);
        }


        
        //private byte[] GenerateSalt()
        //{

        //}

        private string  ComputeHash(string password)
        {
            
            var prf = KeyDerivationPrf.HMACSHA256;
            var rng = RandomNumberGenerator.Create();
            const int iterCount = 10000;
            const int saltSize = 128 / 8;
            const int numBytesRequested = 256 / 8;

            var salt = new byte[saltSize];
            rng.GetBytes(salt);
            var subkey = KeyDerivation.Pbkdf2(password, salt, prf, iterCount, numBytesRequested);

            var outputBytes = new byte[13 + salt.Length + subkey.Length];
            outputBytes[0] = 0x01; // format marker
            WriteNetworkByteOrder(outputBytes, 1, (uint)prf);
            WriteNetworkByteOrder(outputBytes, 5, iterCount);
            WriteNetworkByteOrder(outputBytes, 9, saltSize);
            Buffer.BlockCopy(salt, 0, outputBytes, 13, salt.Length);
            Buffer.BlockCopy(subkey, 0, outputBytes, 13 + saltSize, subkey.Length);
            return Convert.ToBase64String(outputBytes);
        }

        private bool CompareHash(string login, string stored)
        {
            
            var decodedHashedPassword = Convert.FromBase64String(stored);

            // Wrong version
            if (decodedHashedPassword[0] != 0x01)
                return false;

            // Read header information
            var prf = (KeyDerivationPrf)ReadNetworkByteOrder(decodedHashedPassword, 1);
            var iterCount = (int)ReadNetworkByteOrder(decodedHashedPassword, 5);
            var saltLength = (int)ReadNetworkByteOrder(decodedHashedPassword, 9);

            // Read the salt: must be >= 128 bits
            if (saltLength < 128 / 8)
            {
                return false;
            }
            var salt = new byte[saltLength];
            Buffer.BlockCopy(decodedHashedPassword, 13, salt, 0, salt.Length);

            // Read the subkey (the rest of the payload): must be >= 128 bits
            var subkeyLength = decodedHashedPassword.Length - 13 - salt.Length;
            if (subkeyLength < 128 / 8)
            {
                return false;
            }
            var expectedSubkey = new byte[subkeyLength];
            Buffer.BlockCopy(decodedHashedPassword, 13 + salt.Length, expectedSubkey, 0, expectedSubkey.Length);

            // Hash the incoming password and verify it
            var actualSubkey = KeyDerivation.Pbkdf2(login, salt, prf, iterCount, subkeyLength);
            return actualSubkey.SequenceEqual(expectedSubkey);



            //SequenceEqual() provided by LINQ
            //return stored.SequenceEqual(login);
        }

        private static void WriteNetworkByteOrder(byte[] buffer, int offset, uint value)
        {
            buffer[offset + 0] = (byte)(value >> 24);
            buffer[offset + 1] = (byte)(value >> 16);
            buffer[offset + 2] = (byte)(value >> 8);
            buffer[offset + 3] = (byte)(value >> 0);
        }

        private static uint ReadNetworkByteOrder(byte[] buffer, int offset)
        {
            return ((uint)(buffer[offset + 0]) << 24)
                | ((uint)(buffer[offset + 1]) << 16)
                | ((uint)(buffer[offset + 2]) << 8)
                | ((uint)(buffer[offset + 3]));
        }

    }




}
