using System.Windows;
using System.Windows.Controls;
using MailClient.viewmodels;

namespace MailClient.views;

public partial class Registration : UserControl
{
    private readonly InputViewModel _registration;
    public Registration()
    {
        InitializeComponent();
        _registration = new InputViewModel();
        DataContext = _registration;
}

    private async void Register(object sender, RoutedEventArgs routedEventArgs)
    {
        await _registration.SaveRegistrationData();
    }
}