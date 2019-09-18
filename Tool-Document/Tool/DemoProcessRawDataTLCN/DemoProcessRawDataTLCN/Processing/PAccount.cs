using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using DemoProcessRawDataTLCN.BUS;
using DemoProcessRawDataTLCN.Models;

namespace DemoProcessRawDataTLCN.Processing
{
    public class PAccount
    {
        private AccountBAL accountBal;

        public PAccount()
        {
            accountBal = new AccountBAL();
        }

        public void Execute()
        {
            var ds = accountBal.GetListAccount();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                var salt = CreateSalt();
                var account = new Account()
                {
                    ID = int.Parse(ds.Tables[0].Rows[i][0].ToString()),
                    Username = ds.Tables[0].Rows[i][1].ToString(),
                    Password = GenerateHash(ds.Tables[0].Rows[i][2].ToString(), salt),
                    Salt = salt,
                    Email = ds.Tables[0].Rows[i][3].ToString(),
                    CrearedDateTime =
                        GenerateDateTime(Convert.ToDateTime(ds.Tables[0].Rows[i][4]).ToString("yyyy-MM-dd"),
                            ds.Tables[0].Rows[i][5].ToString()),
                };
                accountBal.InsertAccount(account);
                Console.WriteLine(account.ID);
            }
        }

        public string CreateSalt()
        {
            //Generate a cryptographic random number.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[32];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        public string GenerateHash(string input, string salt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input + salt);
            SHA256Managed sHA256ManagedString = new SHA256Managed();
            byte[] hash = sHA256ManagedString.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public bool AreEqual(string plainTextInput, string hashedInput, string salt)
        {
            string newHashedPin = GenerateHash(plainTextInput, salt);
            return newHashedPin.Equals(hashedInput);
        }

        public DateTime GenerateDateTime(string date, string time)
        {
            string datatime = string.Concat(date, " ", time);
            return DateTime.ParseExact(datatime, "yyyy-MM-dd HH:mm:ss",
            System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
