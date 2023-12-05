using System.Windows;
using MailClient.viewmodels;

namespace MailClient.views;

public partial class SendMailView
{

    private readonly SendMailViewModel? _sendMailViewModel;
    private string? _mailSendingStatus;
    public SendMailView()
    {
        InitializeComponent();
        _sendMailViewModel = new SendMailViewModel();
        MailTextBox.DataContext = _sendMailViewModel;
    }
    
    private async void SendMailToViewModel(object sender, RoutedEventArgs e)
    {
        _mailSendingStatus = await _sendMailViewModel?.SendMail()!;
        if (_mailSendingStatus != null &&_mailSendingStatus.Contains("erfolgreich"))
        {
            Close();
        }
        else
        {
            MessageBox.Show($"{_mailSendingStatus}");
        }
    }
}