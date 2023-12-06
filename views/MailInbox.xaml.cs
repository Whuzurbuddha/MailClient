using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using MailClient.DataController;
using static MailClient.DataController.EmailController;

namespace MailClient.views;

public partial class MailInbox : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
        
    private string? _recipient;
    private string? _regarding;

    private string? _mailText;
    private static readonly EmailController MessageOverview =  new EmailController();
    private List<Message>? _mailList = MessageOverview.MessagesOverview;

    public List<Message>? MailList
    {
        get => _mailList;
        set
        {
            _mailList = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MailList)));
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
    public string? Regarding
    {
        get => _regarding;
        set
        {
            _regarding = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Regarding)));
        }
    }public string? Recipient
    {
        get => _recipient;
        set
        {
            _recipient = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Recipient)));
        }
    }

    
    public MailInbox()
    {
        InitializeComponent();
        LoadMailOverview();
    }
    
    private void ShowSelectedMailText(object sender, RoutedEventArgs e)
    {
        ReceivedMailText.Visibility = Visibility.Visible;
        ReceivedMailText.Items.Clear();
        if(MailList != null)
            foreach (var mail in MailList)
            {
                var subject = mail.MessageSubject;
                if (sender is ListViewItem { IsSelected: true })
                {
                    _recipient = sender.ToString();
                    if (_recipient != null && subject != null && _recipient.Contains(subject))
                    {
                        Recipient = mail.MessageSender;
                        Regarding = mail.MessageSubject;
                        MailText = mail.MessageText;
                    }
                }
            }
        ReceivedMailText.Items.Add(MailText);
    }
    
    private void LoadMailOverview()
    {
        if(MailList != null)
            foreach (var mail in MailList)
            {
                var subject = mail.MessageSubject;
                var mailSender = mail.MessageSender;
                var subjectItem = new ListViewItem { Content = $"{mailSender}\r\n{subject}" };
                if(subject != string.Empty)
                  ReceivedMailOverview.Items.Add(subjectItem);
            }
    }
}
