using System.Windows;

namespace MailClient.views;

public partial class UserPage : Window
{
    public UserPage()
    {
        InitializeComponent();
    }

    private SendMailView? _newMailWindow;
    private void OpenNewMailWindow(object sender, RoutedEventArgs e)
    {
        _newMailWindow = new SendMailView();
        _newMailWindow.Show();
    }
}