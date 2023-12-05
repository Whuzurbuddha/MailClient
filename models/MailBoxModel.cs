using System.Collections.Generic;
using System.ComponentModel;
using MailClient.DataController;
using MimeKit;

namespace MailClient.models;

public class MailBoxModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private List<MailBox.Message>? _mailList;
    private InternetAddressList? _messageSender;
    private string? _regarding;
    private string? _mailText;

    public List<MailBox.Message>? MailList
    {
        get => _mailList;
        set
        {
            _mailList = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(MailList?.ToString()));
        }
    }
    public InternetAddressList? MessageSender
    {
        get => _messageSender;
        set
        {
            _messageSender = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MessageSender)));
        }
    }
    public string? Regarding
    {
        get => _regarding;
        set
        {
            _regarding = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Regarding)));
        }
    }
    public string? MailText
    {
        get => _mailText;
        set
        {
            _mailText = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MailText)));
        }
    }

    public void LoadMailList()
    {
        MailList = MailBox.GetMailBox();
        if(MailList != null)
            foreach (var mail in MailList)
            {
                MessageSender = mail.MessageSender;
                Regarding = mail.MessageSubject;
                MailText = mail.MessageText;
            }
    }
}

