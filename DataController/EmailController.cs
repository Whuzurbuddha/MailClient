using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using MailKit;
using MailKit.Net.Imap;
using MimeKit;

namespace MailClient.DataController
{
    public class EmailController
    {
        private ObservableCollection<MailItem>? _messagesOverview;
        public async Task<ObservableCollection<MailItem>?> ReceivingMailAsync()
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
                        Attachments = (await inbox.GetMessageAsync(i)).Attachments
                    };
                    
                    _messagesOverview.Add(message);
                }

                await client.DisconnectAsync(true);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return _messagesOverview ?? null;
        }
        
        public class MailItem
        {
            public string? MessageId { get; set; }
            public string? MessageSubject { get; init; }
            public string? MessageSender { get; init; }
            public string? MessageText { get; init; }
            public IEnumerable<MimeEntity>? Attachments { get; init; }
        }
    }
}
