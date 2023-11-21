using System;
using System.IO;
using System.Text.Json;

namespace MailClient.DataController
{
    public class ReadJson
    {
        private static string filePath = "C:\\Users\\alexander\\RiderProjects\\MailClient\\MailClient\\Data\\SavedData.json";

        public string Passwd { get; set; }
        public string UserName { get; set; }
        public string Smtp { get; set; }
        public string Imap { get; set; }

        public static string GetUserPasswd()
        {
            try
            {
                string jsonContent;

                using (StreamReader reader = new StreamReader(filePath))
                {
                    jsonContent = reader.ReadToEnd();
                }

                ReadJson? readJson = JsonSerializer.Deserialize<ReadJson>(jsonContent);
                string passwd = readJson.Passwd;
                return passwd;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Lesen der JSON-Datei: {ex.Message}");
                return null;
            }
        }
    }
}