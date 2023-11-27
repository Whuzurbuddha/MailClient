using System.Windows;
using MailClient.viewmodels;

namespace MailClient.views;

public partial class SendMailView
{
    private readonly SendMailViewModel _sendMailViewModel;
    public SendMailView()
    {
        InitializeComponent();
        _sendMailViewModel = new SendMailViewModel();
        DataContext = _sendMailViewModel;
    }

    private async void SendMailToViewModel(object sender, RoutedEventArgs e)
    {
        var result = await SendMailViewModel.SendMail();
        if(result) this.Close();
    }
}