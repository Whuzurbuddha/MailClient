using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using MailKit;
using MailKit.Net.Imap;

namespace MailClient.DataController
{
    public class EmailController
    {
        private ObservableCollection<MailItem>? _messagesOverview;

        public async Task<ObservableCollection<MailItem>>ReceivingMailAsync()
        {
            var userContent = ReadJson.GetUserContent();
            var userMail = userContent.User;
            var encryptedPasswd = userContent.EncryptedPasswd;
            var password = ContentManager.DecryptedPasswd(encryptedPasswd);
            var imap = userContent.Imap;
            _messagesOverview = new ObservableCollection<MailItem>();

            using var client = new ImapClient();

            try
            {
                await client.ConnectAsync(imap, 993, true);
                await client.AuthenticateAsync(userMail, password);
                var inbox = client.Inbox;
                await inbox.OpenAsync(FolderAccess.ReadOnly);
                for (var i = 0; i < inbox.Count; i++)
                {
                    var message = new MailItem
                    {
                        MessageId = (await inbox.GetMessageAsync(i)).MessageId,
                        MessageSubject = (await inbox.GetMessageAsync(i)).Subject,
                        MessageSender = (await inbox.GetMessageAsync(i)).From.ToString(),
                        MessageText = (await inbox.GetMessageAsync(i)).TextBody,
                    };
                    _messagesOverview.Add(message);
                }

                await client.DisconnectAsync(true);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return _messagesOverview;
        }
        
        public class MailItem
        {
            public string? MessageSender { get; init; }
            public string? MessageSubject { get; init; }
            public string? MessageId { get; set; }
            public string? MessageText { get; init; }
        }
    }
}
