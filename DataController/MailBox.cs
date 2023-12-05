using System.Collections.Generic;
using System.ComponentModel;
using MimeKit;
namespace MailClient.DataController;

public abstract class MailBox
{
    public class Message : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private readonly string? _messageSubject;
        private readonly InternetAddressList? _messageSender;
        private readonly string? _messageText;
        public string? MessageSubject
        {
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

    private static List<EmailController.Message>? _mailInbox;
    private static List<Message>? _mailList;
    private static Message? _singleMail;
   
    public static List<Message>? GetMailBox()
    {
        _mailList = new List<Message>();
        _mailInbox = EmailController.ReceivingMail();
        if (_mailInbox == null) return null;
        foreach (var mail in _mailInbox)
        {
            _singleMail = new Message()
            {
                MessageSender = mail.MessageSender,
                MessageSubject = mail.MessageSubject,
                MessageText = mail.MessageText
            };
            _mailList?.Add(_singleMail);
        }
        return _mailList;
    }
}
