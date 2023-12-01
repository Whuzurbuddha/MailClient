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

    private string? Sender
    {
        get => _sender;
        set
        {
            _sender = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Sender));
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
        var message = "";
        ReceivedMailText.Items.Clear();
        ReceivedMailOverview.Items.Clear();
        ReceivedMailText.Items.Refresh();
        ReceivedMailOverview.Items.Refresh();
        foreach (var mail in mailList)
        {
            var subject = new ListViewItem { Content = mail.MessageSubject };
            ReceivedMailOverview.Items.Add(subject);
            ReceivedMailOverview.Items.Refresh();
            if (Sender == mail.MessageSubject)
            {
                message += mail.MessageText;
            }
        }

        ReceivedMailText.Items.Add(message);
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

/*if (sender is ListViewItem { IsSelected: true } && sender == subject)
{
    message += messageText;
}*/