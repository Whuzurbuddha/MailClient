using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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

    private async Task SendMailToViewModel(object sender, RoutedEventArgs e)
    {
        var result = await SendMailViewModel.SendMail()!;
    }
}