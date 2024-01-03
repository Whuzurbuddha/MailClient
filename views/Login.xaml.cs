using System.Windows;
using System.Windows.Controls;
using MailClient.viewmodels;

namespace MailClient.views;

public partial class Login : UserControl
{
    private readonly InputViewModel _login;
    public Login()
    {
        InitializeComponent();
        _login = new InputViewModel();
        DataContext = _login;
    }
    private void SendLogin(object sender, RoutedEventArgs routedEventArgs)
    {
        _login.SendLoginData();
    }
}