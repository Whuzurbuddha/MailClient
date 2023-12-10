using System.Windows;
using MailClient.Models;
using MailClient.viewmodels;

namespace MailClient.views;

public partial class SendMailView
{
    private readonly MailContentViewModel? _sendMailViewModel;
    private static MailInbox? _mailInbox;
    private static GetMailViewModel? _getMailViewModel;
    private string? _mailSendingStatus;
    public SendMailView()
    {
        InitializeComponent();
        _sendMailViewModel = new MailContentViewModel();
        DataContext = _sendMailViewModel;
    }
    private async void SendMailToViewModel(object sender, RoutedEventArgs e)
    {
        _getMailViewModel = new GetMailViewModel();
        _mailInbox = new MailInbox();
        _mailSendingStatus = await _sendMailViewModel?.SendMail()!;
        if (_mailSendingStatus != null && _mailSendingStatus.Contains("erfolgreich") || _mailSendingStatus!.Contains("true"))
        {
            var result = _getMailViewModel.GenerateMailLists();
            if (result.ToString() == string.Empty) return;
            var refreshed = _mailInbox.RefreshMailBoxView();
            if (refreshed.Result == false) return;
            Close();
        }
        else
        {
            MessageBox.Show($"{_mailSendingStatus}");
        }
    }

    private void ShowFileLoader(object sender, RoutedEventArgs e)
    {
        _sendMailViewModel?.GetFilePath();
    }
}