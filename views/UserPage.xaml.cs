﻿using System;
 using System.Collections.ObjectModel;
 using System.IO;
using System.Windows;
using System.Windows.Controls;
using MailClient.Models;
 using MailClient.viewmodels;
 using static MailClient.DataController.ReadMailCache;

namespace MailClient.views;

public partial class UserPage
{
    public UserPage()
    {
        InitializeComponent();
        if (!CheckIfAccountExits()) return;
        DownloadMails();
        LoadMailOverview();
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

    private void SetMailSender(object sender, RoutedEventArgs e)
    {
        if (sender is not TreeViewItem {IsExpanded: true} provider) return;
        if (provider.DataContext is UserContent mailAccount)
        {
            (DataContext as MailContentViewModel)?.SetSelectedMailProvider(mailAccount.MailAddress, mailAccount.Smtp, mailAccount.EncryptedPwd);
        }
    }
    private void ChooseMailbox(object sender, RoutedEventArgs e)
    {
        if (sender is not DockPanel inbox) return;
        if (inbox.DataContext is UserContent userContent)
        {
            MailInbox.SetMailbox(userContent.Mailbox);
        }
    }

    public void DownloadMails()
    {
        (DataContext as GetMailViewModel)?.GetMailsFromServer();
    }

    private void LoadMailOverview()
    {
        (DataContext as GetMailViewModel)?.GetUserAccounts();
    }

    public void RefreshProviderOverview()
    {
        var currentContent = ProviderOverview.DataContext;
        ProviderOverview.Items.Remove(currentContent);
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