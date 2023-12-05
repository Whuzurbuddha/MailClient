using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Imap;
using MimeKit;

namespace MailClient.DataController;
//SMTP smtp.web.de
//IMAP imap.web.de
public class EmailController
{
    public static async Task<bool> SendingMail(string? recipient, string? regarding, string? mailContent)
    {
        var userContent = ReadJson.GetUserContent();
        var userMail = userContent.User;
        var smtp = userContent.Smtp;
        var encryptedPasswd = userContent.EncryptedPasswd;
        var password = ContentManager.DecryptedPasswd(encryptedPasswd);
        var smtpClient = new SmtpClient(smtp)
        {
            Port = 587, 
            Credentials = new NetworkCredential(userMail, password),
            EnableSsl = true
        };
        if (userMail == null || recipient == null || mailContent == null) return false;
        smtpClient.Send(userMail, recipient, regarding, mailContent);
        return true;
    }
    
    public class Message : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private readonly string? _messageSubject;
        private readonly InternetAddressList? _messageSender;
        private readonly string? _messageText;
        public string? MessageSubject { 
            get => _messageSubject;
            init
            {
                _messageSubject = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(MessageSubject));
            }
            
        }

        public InternetAddressList? MessageSender
        {
            get => _messageSender;
            init
            {
                _messageSender = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(MessageSender?.ToString()));
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

    public static List<Message>? ReceivingMail()
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
                    MessageSender = inbox.GetMessage(i).From,
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