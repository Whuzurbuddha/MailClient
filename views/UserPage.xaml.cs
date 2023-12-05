using System.Windows;
using MailClient.DataController;
using MailClient.models;

namespace MailClient.views;

public partial class UserPage : Window
{
    public UserPage()
    {
        InitializeComponent();
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
        //_answerMailView = new AnswerMailView();
        _answerMailView = new SendMailView();
        _answerMailView.Show();
    }

    private void ShowMailBox(object sender, RoutedEventArgs routedEventArgs)
    {
        MailInbox.Visibility = Visibility.Visible;
    }
}