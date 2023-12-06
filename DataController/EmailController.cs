using System;
using System.Collections.Generic;
using System.ComponentModel;
using MailKit;
using MailKit.Net.Imap;

namespace MailClient.DataController;
//SMTP smtp.web.de
//IMAP imap.web.de
public class EmailController : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    
    private List<Message>? _messagesOverview = ReceivingMail();

    public List<Message>? MessagesOverview
    {
        get => _messagesOverview;
        set
        {
            _messagesOverview = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(MessagesOverview?.ToString()));
        }
    }

    public class Message
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private readonly string? _messageSubject;
        private readonly string? _messageSender;
        private readonly string? _messageText;
        
        public string? MessageSubject { 
            get => _messageSubject;
            init
            {
                _messageSubject = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(MessageSubject));
            }
            
        }

        public string? MessageSender
        {
            get => _messageSender;
            init
            {
                _messageSender = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(MessageSender));
            }
        }
        public string? MessageId { get; set; }

        public string? MessageText
        {
            get => _messageText;
            init
            {
                _messageText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(MessageText));
            }
        }
    }

    private static List<Message>? ReceivingMail()
    {
        using var client = new ImapClient();
        var userContent = ReadJson.GetUserContent();
        var userMail = userContent.User;
        var encryptedPasswd = userContent.EncryptedPasswd;
        var password = ContentManager.DecryptedPasswd(encryptedPasswd);
        var imap = userContent.Imap;
        var messagesList = new List<Message>();
        
        try
        {
            client.Connect(imap, 993, true);
            client.Authenticate(userMail, password);
            var inbox = client.Inbox;
            inbox.Open(FolderAccess.ReadOnly);
            
            for (var i = 0; i < inbox.Count; i++)
            {

                var messagesOverview = new Message()
                {
                    MessageId = inbox.GetMessage(i).MessageId,
                    MessageSubject = inbox.GetMessage(i).Subject,
                    MessageSender = inbox.GetMessage(i).From.ToString(),
                    MessageText = inbox.GetMessage(i).TextBody
                };
               messagesList.Add(messagesOverview);
            }
            client.Disconnect(true);
            return messagesList;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}