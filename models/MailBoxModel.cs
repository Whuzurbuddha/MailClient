/*
using System.Collections.Generic;
using System.ComponentModel;
using MailClient.DataController;

namespace MailClient.models;

public class MailBoxModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private List<MailBox.Message>? _mailList;
    private string? _recipient;
    private string? _regarding;
    private string? _mailText;

    private List<MailBox.Message>? MailList
    {
        get => _mailList;
        set
        {
            _mailList = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(MailList?.ToString()));
        }
    }
    public string? Recipient
    {
        get => _recipient;
        set
        {
            _recipient = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Recipient)));
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
        //MailList = MailBox.GetMailBox();
        if(MailList != null)
            foreach (var mail in MailList)
            {
                Recipient = mail.MessageSender;
                Regarding = mail.MessageSubject;
                MailText = mail.MessageText;
            }
    }

}
*/

