using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace MailClient.DataController;

public class ReadMailAccountJson
{
        public string? UserMail { get; set; }
        public string? Passwd { get; set; }
        public string? Smtp { get; set; }
        public string? Imap { get; set; }

        public static async Task GetUserContent()
        {
            var mainDirectory = ConstPaths.MainDirectory;
            var mailAccounts = Directory.GetDirectories($@"{mainDirectory}\\mailaccounts"!);

            if (mailAccounts.Length == 0) return;
            
            for (var i = 0; i < mailAccounts.Length; i++)
            {
                if (mailAccounts[i].Contains("Data")) continue;
                var filePath = mailAccounts[i];
                try
                {
                    using var reader = new StreamReader($"{filePath}\\Account.json");
                    var jsonContent = await reader.ReadToEndAsync();
                    var readJson = JsonSerializer.Deserialize<ReadMailAccountJson>(jsonContent);
                    var userMail = readJson?.UserMail;
                    var smtp = readJson?.Smtp;
                    var imap = readJson?.Imap;
                    var encryptedPassword = readJson?.Passwd;
                    var passwd = ContentManager.DecryptPasswd(encryptedPassword);
                    var controller = new EmailController();
                    var accountName = filePath.Split($@"\")[7];
                    await controller.ReceivingMailAsync(accountName,imap, userMail, passwd);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                    throw;
                }
            }
        }
}
