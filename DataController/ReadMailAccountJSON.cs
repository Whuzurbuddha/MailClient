using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.VisualBasic.FileIO;

namespace MailClient.DataController;

public class ReadMailAccountJson
{
        public string? UserMail { get; set; }
        public string? Passwd { get; set; }
        public string? Smtp { get; set; }
        public string? Imap { get; set; }

        public static ObservableCollection<UserContent>? UserAccounts;
        public class UserContent
        {
            public string? AccountName { get; set; }
            public string? UserMail { get; set; }
            public string? Passwd { get; set; }
            public string? Smtp { get; set; }
            public string? Imap { get; set; }
            public ObservableCollection<EmailController.MailItem>? Mailbox { get; init; }
        }
        public static async Task<ObservableCollection<UserContent>?> GetUserContent()
        {
            UserAccounts = new ObservableCollection<UserContent>();
            var mainDirectory = ConstPaths.MainDirectory;
            var clientAccount = Directory.GetDirectories(mainDirectory!);
            var mailAccounts = Directory.GetDirectories(clientAccount[0]);

            if (mailAccounts.Length == 0) return null;
            
            for (var i = 0; i < mailAccounts.Length; i++)
            {
                if (mailAccounts[i].Contains("Data")) continue;
                var filePath = mailAccounts[i];
                try
                {
                    using var reader = new StreamReader($"{filePath}\\Account.json");
                    var jsonContent = await reader.ReadToEndAsync();
                    var readJson = JsonSerializer.Deserialize<ReadMailAccountJson>(jsonContent);
                    var user = readJson?.UserMail;
                    var smtp = readJson?.Smtp;
                    var imap = readJson?.Imap;
                    var passwd = readJson?.Passwd;
                    var controller = new EmailController();
                    var accountName = filePath.Split($"\\")[6];
                    var mailBox = await controller.ReceivingMailAsync(accountName,imap, user, passwd);
                    var userContent = new UserContent
                    {
                        AccountName = accountName,
                        UserMail = user,
                        Smtp = smtp,
                        Imap = imap,
                        Passwd = passwd,
                        Mailbox = mailBox
                    };
                        
                    UserAccounts.Add(userContent);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                    throw;
                }
            }
            return UserAccounts;
        }
}
