using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MailClient.DataController;

namespace MailClient.views;

public partial class MailInbox : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private string? _sender;
    private string? _message;

    private string? Sender
    {
        get => _sender;
        set
        {
            _sender = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Sender));
        }
    }

    private string? Message
    {
        get => _message;
        set
        {
            _message = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Message));
        }
    }
    
    public MailInbox()
    {
        InitializeComponent();
        ReceivedMailsOverview();
    }
    
    private void ReceivedMailsOverview()
    {
        var mailList = EmailController.ReceivingMail();
        ReceivedMailText.Items.Clear();
        ReceivedMailOverview.Items.Clear();
        ReceivedMailText.Items.Refresh();
        ReceivedMailOverview.Items.Refresh();
        foreach (var mail in mailList)
        {
            var subject = mail.MessageSubject;
            var subjectItem = new ListViewItem { Content = subject };
            ReceivedMailOverview.Items.Add(subjectItem);
            ReceivedMailOverview.Items.Refresh();
            if (Sender != null && subject != null && Sender.Contains(subject))
            {
                Message = mail.MessageText;
            }
        }
        ReceivedMailText.Items.Add(Message);
    }

    private void ShowSelectedMailText(object sender, RoutedEventArgs e)
    {
        ReceivedMailText.Visibility = Visibility.Visible;
        if (sender is ListViewItem { IsSelected: true })
        {
            Sender = sender.ToString();
        }
        ReceivedMailsOverview();
    }
}
