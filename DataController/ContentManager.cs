using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using MailClient.views;

namespace MailClient.DataController
{
    public class ContentManager
    {
        private class RegistrationData
        {
            public string? UserName { get; set; }
            public string? Passwd { get; set; }
            public string? Smtp { get; set; }
            public string? Imap { get; set; }
        }

        public static async Task SaveRegistration(string? UserName, string? Password, string? Smtp, string? Imap)
        {
            var encryptedPassword = await EncryptedPasswd(Password)!;
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Smtp) || string.IsNullOrEmpty(Imap))
            {
                Console.WriteLine("Invalid data. Cannot save.");
                return;
            }

            var registrationData = new RegistrationData
            {
                UserName = UserName,
                Passwd = encryptedPassword,
                Smtp = Smtp,
                Imap = Imap
            };

            const string filePath = @"C:\Users\PaeplowA\RiderProjects\MailClient\Data\SavedData.json";
            var json = JsonSerializer.Serialize(registrationData);

            await using var writer = new StreamWriter(filePath);
            await writer.WriteAsync(json);
        }

        public static void CheckLogin(string userName, string? password)
        {
            var userContent = ReadJson.GetUserContent();
            var encryptedPassword = userContent.EncryptedPasswd;
            var decryptedPasswd = DecryptedPasswd(encryptedPassword);
            var userWindow = new UserPage();
            if (password == decryptedPasswd)
            {
                if (Application.Current.MainWindow != null) Application.Current.MainWindow.Close();  userWindow.Show();
            }
            else
            {
                MessageBox.Show("Ungültige Zugangsdaten");
            }
        }

        public static string? DecryptedPasswd(string? encryptedPassword)
        {
            if (encryptedPassword == null) return null;

            byte[] encryptedData = Convert.FromBase64String(encryptedPassword);
            byte[] decryptedData = ProtectedData.Unprotect(encryptedData, null, DataProtectionScope.CurrentUser);
            
            return Encoding.Unicode.GetString(decryptedData);
        }

        private static Task<string>? EncryptedPasswd(string? password)
        {
            if (password == null) return null;
            var data = Encoding.Unicode.GetBytes(password);
            byte[] encrypted = ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser);
            
            return Task.FromResult(Convert.ToBase64String(encrypted));
        }
    }
}