using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using MailClient.views;
using Microsoft.VisualBasic.FileIO;

namespace MailClient.DataController
{
    public class ContentManager
    {
        private class RegistrationData
        {
            public string? UserName { get; set; }
            public string? Passwd { get; set; }
        }

        private static System.IO.DirectoryInfo? CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
            return null;
        }

        public static async Task SaveRegistration(string?  userName, string? password)
        {
            var encryptedPassword = await EncryptPasswd(password)!;
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Invalid data. Cannot save.");
                return;
            }

            var registrationData = new RegistrationData
            {
                UserName = userName,
                Passwd = encryptedPassword,
            };
            var mainDir = ConstPaths.MainDirectory;
            var userDirectory = new StringBuilder();
            userDirectory.AppendFormat($@"{mainDir}\\mainaccount\\{userName}");
            if (Directory.Exists(userDirectory.ToString()))
            {
                MessageBox.Show("Account already exists");
            }
            else
            {
                CreateDirectory(userDirectory.ToString());
                CreateDirectory($"{userDirectory}\\Data");

                var filePath = new StringBuilder();
                filePath.AppendFormat($"{userDirectory}\\Data\\SavedData.json");
            
                var json = JsonSerializer.Serialize(registrationData);

                await using var writer = new StreamWriter(filePath.ToString());
                await writer.WriteAsync(json);
                writer.Close();
                MessageBox.Show("Created new account successfully");
            }
        }

        public static void CheckLogin(string? userName, string? password)
        {
            var userContent = ReadMainAccountJson.GetUserContent();
            if (userContent == null) return;
            var encryptedPassword = userContent?.EncryptedPasswd;
            var decryptedPasswd = DecryptPasswd(encryptedPassword);
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

        public static string? DecryptPasswd(string? encryptedPassword)
        {
            if (encryptedPassword == null) return null;

            byte[] encryptedData = Convert.FromBase64String(encryptedPassword);
            byte[] decryptedData = ProtectedData.Unprotect(encryptedData, null, DataProtectionScope.CurrentUser);
            return Encoding.Unicode.GetString(decryptedData);
        }

        public static Task<string>? EncryptPasswd(string? password)
        {
            if (password == null) return null;
            var data = Encoding.Unicode.GetBytes(password);
            byte[] encrypted = ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser);
            
            return Task.FromResult(Convert.ToBase64String(encrypted));
        }
    }
}
