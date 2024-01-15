using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MailClient.DataController;
using MailClient.models;
using MailClient.Models;

namespace MailClient.views;

public partial class UserPage
{
    private static Loading? _loading;
    public UserPage()
    {
        InitializeComponent();
        _loading = new Loading();
        Loaded += ShowLoading;
        LoadMailAccounts();
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
    private Calendar? _calendar;
    
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

    private void OpenCalendar(object sender, RoutedEventArgs e)
    {
        _calendar = new Calendar();
        _calendar.Show();
    }

    private void ChooseMailbox(object sender, RoutedEventArgs e)
    {
        Console.WriteLine("SELECTED MAILBOX");
        
        if (sender is not TreeViewItem treeViewItem) return;
        if (treeViewItem.DataContext is ReadMailAccountJSON.UserContent userContent)
        {
            MailInbox.SetMailbox(userContent.Mailbox);
        }
    }
    private void LoadMailAccounts()
    {
        (DataContext as GetMailViewModel)?.GenerateAccountOverview();
    }

    public void RefreshProviderOverview()
    {
        ProviderOverview.Items.Remove((DataContext as GetMailViewModel)?.UserAccounts);
        ProviderOverview.Items.Add((DataContext as GetMailViewModel)?.UserAccounts);
        ProviderOverview.Items.Refresh();
    }
}
