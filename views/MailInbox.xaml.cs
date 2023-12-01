using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MailClient.DataController;

namespace MailClient.views;

public partial class MailInbox : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private string? _sender;
    private string? _mailText;

    private string? Sender
    {
        get => _sender;
        set
        {
            _sender = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Sender));
        }
    }

    public string? MailText
    {
        get => _mailText;
        set
        {
            _mailText = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(MailText));
        }
    }

    private readonly List<EmailController.Message> _mailList;
    public MailInbox()
    {
        InitializeComponent();
        _mailList = EmailController.ReceivingMail();
        LoadMailOverview(_mailList);
    }
    
    private void ShowSelectedMailText(object sender, RoutedEventArgs e)
    {
        ReceivedMailText.Visibility = Visibility.Visible;
        ReceivedMailText.Items.Clear();
        foreach (var mail in _mailList)
        {
            var subject = mail.MessageSubject;
            if (sender is ListViewItem { IsSelected: true })
            {
                Sender = sender.ToString();
                if (Sender != null && subject != null && Sender.Contains(subject))
                {
                    MailText = mail.MessageText;
                }
            }
        }
        ReceivedMailText.Items.Add(MailText);
    }

    private void LoadMailOverview(List<EmailController.Message> mailList)
    {
        foreach (var mail in mailList)
        {
            var subject = mail.MessageSubject;
            var mailSender = mail.MessageSender;
            var subjectItem = new ListBoxItem { Content = $"{mailSender}\r\n{subject}" };
            ReceivedMailOverview.Items.Add(subjectItem);
        }
    }

    private string? _selectedMail;
    public string? SelectedMail()
    {
        _selectedMail = MailText ?? "";
        return _selectedMail;
    }
}
