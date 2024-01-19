using System;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MailClient.DataController;
using MailClient.models;
using MailClient.Models;
using Microsoft.VisualBasic;
using static MailClient.DataController.ReadMailCache;

namespace MailClient.views;

public partial class UserPage
{
    public UserPage()
    {
        InitializeComponent();
        if (!CheckIfAccountExits()) return;
        LoadMailOverview();
        //DownloadMails();
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
        //_answerMailView.SetAnswerText();
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
        if (sender is not TreeViewItem treeViewItem) return;
        if (treeViewItem.DataContext is UserContent userContent)
        {
            MailInbox.SetMailbox(userContent.Mailbox);
        }
    }
    private void DownloadMails()
    {
        (DataContext as GetMailViewModel)?.GetMailsFromServer();
    }

    private void LoadMailOverview()
    {
        (DataContext as GetMailViewModel)?.GetUserAccounts();
    }

    public void RefreshProviderOverview()
    {
        ProviderOverview.Items.Remove((DataContext as GetMailViewModel)?.UserAccounts);
        ProviderOverview.Items.Add((DataContext as GetMailViewModel)?.UserAccounts);
        ProviderOverview.Items.Refresh();
    }

    private bool CheckIfAccountExits()
    {
        if (!File.Exists($@"{ConstPaths.MainDirectory!}\mailaccounts"))
        {
            Directory.CreateDirectory($@"{ConstPaths.MainDirectory!}\mailaccounts");
        }
        if (!File.Exists($@"{ConstPaths.MainDirectory!}\mainaccount"))
        {
            Directory.CreateDirectory($@"{ConstPaths.MainDirectory!}\mainaccount");
        }

        if (!File.Exists(ConstPaths.CachePath))
        {
            Directory.CreateDirectory(ConstPaths.CachePath!);
        }
        var accounts = Directory.GetDirectories($@"{ConstPaths.MainDirectory!}\mailaccounts");
        if (accounts.Length == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
