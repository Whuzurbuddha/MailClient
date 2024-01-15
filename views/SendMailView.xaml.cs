using System.Windows;
using MailClient.Models;
using MailClient.viewmodels;

namespace MailClient.views;

public partial class SendMailView
{
    private readonly MailContentViewModel? _sendMailViewModel;
    private string? _mailSendingStatus;
    public SendMailView()
    {
        InitializeComponent();
        _sendMailViewModel = new MailContentViewModel();
        DataContext = _sendMailViewModel;
    }
    private async void SendMailToViewModel(object sender, RoutedEventArgs e)
    {
        _mailSendingStatus = await _sendMailViewModel?.SendMail()!;
        if (_mailSendingStatus != null && _mailSendingStatus.Contains("erfolgreich") || _mailSendingStatus!.Contains("true"))
        {
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

    private GetMailViewModel? _getMailViewModel;
    public void SetAnswerText()
    {
        _getMailViewModel = new GetMailViewModel();
        if (_getMailViewModel.SelectedMailText != null) MailBox.Text += _getMailViewModel.SelectedMailText;
    }
}