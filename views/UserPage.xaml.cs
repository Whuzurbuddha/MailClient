using System.Windows;
using System.Windows.Navigation;

namespace MailClient.views;

public partial class UserPage
{
    private static Loading? _loading;
    public UserPage()
    {
        InitializeComponent();
        _loading = new Loading();
        Loaded += ShowLoading;
    }
    
    private void ShowLoading(object sender, RoutedEventArgs e)
    {
        _loading?.Show();
    }

    public static void CloseLoading()
    {
        _loading?.Close();
    }

    private SendMailView? _newMailWindow;
    private SendMailView? _answerMailView;
    
    private void OpenNewMailWindow(object sender, RoutedEventArgs e)
    {
        _newMailWindow = new SendMailView();
        _newMailWindow.Show();
    }

    private void OpenAnswerMailWindow(object sender, RoutedEventArgs e)
    {
        _answerMailView = new SendMailView();
        _answerMailView.Show();
    }
    private void ShowMailBox(object sender, RoutedEventArgs routedEventArgs)
    {
        MailInbox.Visibility = Visibility.Visible;
    }
}