using System;
using System.Windows;
using MailClient.viewmodels;

namespace MailClient.views;

public partial class AnswerMailView
{
    public AnswerMailView()
    {
        var sendMail = new SendMailViewModel();
        InitializeComponent();
        ShowSelectedMail();
        DataContext = sendMail;
    }

    private void ShowSelectedMail()
    {
        var mail = MailInbox.SelectedMailText;
        SelectedMailBox.Text = mail.MailText;
    }
    private void SendAnswer(object sender, RoutedEventArgs routedEventArgs){}
}