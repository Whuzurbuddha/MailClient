using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.VisualBasic.FileIO;

namespace MailClient.DataController;

public class SaveAccountData
{
     private class RegistrationData
        {
            public string? UserMail { get; set; }
            public string? Passwd { get; set; }
            public string? Smtp { get; set; }
            public string? Imap { get; set; }
        }

        private static System.IO.DirectoryInfo CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
            return null;
        }

        public static async Task<bool> SaveMailAccount(string?  accountName, string? userMail, string? password, string? smtp, string? imap)
        {
            if (string.IsNullOrEmpty(accountName) || string.IsNullOrEmpty(password))
            {
                Console.WriteLine("Invalid data. Cannot save.");
                return false;
            }

            var registrationData = new RegistrationData
            {
                UserMail = userMail,
                Passwd = password,
                Smtp = smtp,
                Imap = imap
            };
            var documentDirectory = SpecialDirectories.MyDocuments;
            var userDirectory = new StringBuilder();
            userDirectory.AppendFormat($"{documentDirectory}\\MailClient\\");
            string[] user = Directory.GetDirectories(userDirectory.ToString());
            CreateDirectory($"{user[0]}\\{accountName}");
            var newAccount = $"{user[0]}\\{accountName}\\Account.json";
            
            var json = JsonSerializer.Serialize(registrationData);

            await using var writer = new StreamWriter(newAccount);
            await writer.WriteAsync(json);
            writer.Close();
            MessageBox.Show("Neues Konto erfolgreich angelegt");
            return true;
        }
}