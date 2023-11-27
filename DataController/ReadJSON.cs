using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace MailClient.DataController
{
    public class ReadJson
    {
        private static string _filePath = "C:\\Users\\PaeplowA\\RiderProjects\\MailClient\\Data\\SavedData.json";

        public string? Passwd { get; set; }
        public string? UserName { get; set; }
        public string? Smtp { get; set; }
        public string? Imap { get; set; }
        
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

        public static List<string?> GetServerContent()
        {
            try
            {
                string jsonContent;
                using (var reader = new StreamReader(_filePath))
                {
                    jsonContent = reader.ReadToEnd();
                }
                var readJson = JsonSerializer.Deserialize<ReadJson>(jsonContent);
                var userContent = new List<string?>();
                var user = readJson?.UserName;
                var smtp = readJson?.Smtp;
                var imap = readJson?.Imap;
                userContent.Add(user);
                userContent.Add(smtp);
                userContent.Add(imap);
                return userContent;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}