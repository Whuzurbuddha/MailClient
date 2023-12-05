using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using MailClient.DataController;
using MailClient.viewmodels;

namespace MailClient.views;

public partial class MailInbox : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
        
    private string? _recipient = new SendMailViewModel().Recipient;

    private string? _mailText = new SendMailViewModel().MailText;

    public string? MailText
    {
        get => _mailText;
        set
        {
            _mailText = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MailText)));
        }
    }
    

    private readonly List<MailBox.Message>? _mailList;
    public MailInbox()
    {
        InitializeComponent();
        _mailList = MailBox.GetMailBox();
        LoadMailOverview(_mailList);
    }
    
    private void ShowSelectedMailText(object sender, RoutedEventArgs e)
    {
        ReceivedMailText.Visibility = Visibility.Visible;
        ReceivedMailText.Items.Clear();
        if(_mailList != null)
            foreach (var mail in _mailList)
            {
                var subject = mail.MessageSubject;
                if (sender is ListViewItem { IsSelected: true })
                {
                    _recipient = sender.ToString();
                    if (_recipient != null && subject != null && _recipient.Contains(subject))
                    {
                        _recipient = mail.MessageSender?.ToString();
                        MailText = mail.MessageText;
                    }
                }
            }
        ReceivedMailText.Items.Add(_mailText);
    }
    
    private void LoadMailOverview(List<MailBox.Message> _mailList)
    {
        if(this._mailList != null)
            foreach (var mail in _mailList)
            {
                var subject = mail.MessageSubject;
                var mailSender = mail.MessageSender;
                var subjectItem = new ListViewItem { Content = $"{mailSender}\r\n{subject}" };
                ReceivedMailOverview.Items.Add(subjectItem);
            }
    }
}
