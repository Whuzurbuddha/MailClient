using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.VisualBasic.FileIO;

namespace MailClient.DataController;

public class ReadMailAccountJSON
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
            public ObservableCollection<EmailController.MailItem>? Mailbox { get; set; }
        }
        public static async Task<ObservableCollection<UserContent>> GetUserContent()
        {
            UserAccounts = new ObservableCollection<UserContent>();
            var mainDirectory = $"{SpecialDirectories.MyDocuments}\\MailClient";
            string[] clientAccount = Directory.GetDirectories(mainDirectory);
            string[] mailAccounts = Directory.GetDirectories(clientAccount[0]);

            for (var i = 0; i < mailAccounts.Length; i++)
            {
                if (!mailAccounts[i].Contains("Data"))
                {
                    Console.WriteLine($"STARTED RECEIVING MAILBOX FOR {mailAccounts[i].Split($"\\")[6]}");
                    var filePath = mailAccounts[i];
                    try
                    {
                        using var reader = new StreamReader($"{filePath}\\Account.json");
                        var jsonContent = await reader.ReadToEndAsync();
                        var readJson = JsonSerializer.Deserialize<ReadMailAccountJSON>(jsonContent);
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
                    Console.WriteLine($"FINISHED ACCOUNT: {mailAccounts[i].Split($"\\")[6]}");
                }
            }
            return UserAccounts;
        }
}