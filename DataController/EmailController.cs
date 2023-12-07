using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using MailKit;
using MailKit.Net.Imap;

namespace MailClient.DataController
{
    public class EmailController : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private ObservableCollection<MailItem>? _messagesOverview;
        public ObservableCollection<MailItem>? MessagesOverview
        {
            get => _messagesOverview;
            set
            {
                _messagesOverview = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MessagesOverview)));
            }
        }

        public async Task<List<MailItem>> ReceivingMailAsync()
        {
            var userContent = ReadJson.GetUserContent();
            var userMail = userContent.User;
            var encryptedPasswd = userContent.EncryptedPasswd;
            var password = ContentManager.DecryptedPasswd(encryptedPasswd);
            var imap = userContent.Imap;
            var messagesList = new List<MailItem>();

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
                    messagesList.Add(message);
                }

                await client.DisconnectAsync(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return messagesList;
        }

        public async Task<ObservableCollection<MailItem>> LoadMessagesAsync()
        {
            var receivedMessages = await ReceivingMailAsync();
            return MessagesOverview = new ObservableCollection<MailItem>(receivedMessages);
        }
        
        public class MailItem
        {
            public string? MessageSender { get; init; }
            public string? MessageSubject { get; init; }
            public string? MessageId { get; set; }
            public string? MessageText { get; set; }
        }
    }
}
