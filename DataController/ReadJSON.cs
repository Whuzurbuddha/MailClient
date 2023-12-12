using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace MailClient.DataController
{
    public class ReadJson
    {
        private static string _filePath = @"C:\\Users\\PaeplowA\\RiderProjects\\MailClient\\Data\\SavedData.json";

        public string? Passwd { get; set; }
        public string? UserName { get; set; }
        public string? Smtp { get; set; }
        public string? Imap { get; set; }

        public class UserContent
        {
            public string? User { get; set; }
            public string? Smtp { get; set; }
            public string? Imap { get; set; }
            public string? EncryptedPasswd { get; set; }
            public string? DecryptedPasswd { get; set; }
        }
        
        public static string? GetUserPasswd()
        {
            try
            {
                string jsonPasswd;

                using (var reader = new StreamReader(_filePath))
                {
                    jsonPasswd = reader.ReadToEnd();
                }

                var readJson = JsonSerializer.Deserialize<ReadJson>(jsonPasswd);
                var passwd = readJson?.Passwd;
                return passwd;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Lesen der JSON-Datei: {ex.Message}");
                return null;
            }
        }

        public static UserContent GetUserContent()
        {
            try
            {
                string jsonContent;
                using (var reader = new StreamReader(_filePath))
                {
                    jsonContent = reader.ReadToEnd();
                }
                var readJson = JsonSerializer.Deserialize<ReadJson>(jsonContent);
                var user = readJson?.UserName;
                var smtp = readJson?.Smtp;
                var imap = readJson?.Imap;
                var encryptedPasswd = readJson?.Passwd;
                var userContent = new UserContent
                {
                    User = user,
                    Smtp = smtp,
                    Imap = imap,
                    EncryptedPasswd = encryptedPasswd
                };
                
                return userContent;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }
        }
    }
}