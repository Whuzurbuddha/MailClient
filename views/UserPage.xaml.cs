using System;
using System.Collections;
using System.Windows;
using System.Windows.Input;
using MailClient.DataController;
using MailClient.models;
using static MailClient.DataController.ReadMailAccountJSON;

namespace MailClient.views;

public partial class UserPage
{
    private static Loading? _loading;
    public UserPage()
    {
        InitializeComponent();
        _loading = new Loading();
        Loaded += LoadMailAccounts;
        //Loaded += ShowLoading;
    }
    
    private void ShowLoading(object sender, RoutedEventArgs e)
    {
        Mouse.OverrideCursor = Cursors.None;
        _loading?.Show();
    }

    public static void CloseLoading()
    {
        Mouse.OverrideCursor = Cursors.Arrow;
        _loading?.Close();
    }

    private SendMailView? _newMailWindow;
    private SendMailView? _answerMailView;
    private AddressBook? _addressBook;
    private NewAccount? _newAccount;
    
    private void OpenNewMailWindow(object sender, RoutedEventArgs e)
    {
        _newMailWindow = new SendMailView();
        _newMailWindow.Show();
    }

    private void OpenAnswerMailWindow(object sender, RoutedEventArgs e)
    {
        _answerMailView = new SendMailView();
        _answerMailView.SetAnswerText();
        _answerMailView.Show();
    }
    private void ShowMailBox(object sender, RoutedEventArgs routedEventArgs)
    {
        MailInbox.Visibility = Visibility.Visible;
    }

    private void OpenAddressBook(object sender, RoutedEventArgs e)
    {
        _addressBook = new AddressBook();
        _addressBook.Show();
    }

    private void CreateNewAccount(object sender, RoutedEventArgs e)
    {
        _newAccount = new NewAccount();
        _newAccount.Show();
    }

    private void LoadMailAccounts(object sender, RoutedEventArgs routedEventArgs)
    {
        (DataContext as AccountOverviewModel)?.GenerateAccountOverview();
    }
}