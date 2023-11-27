﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using MailClient.DataController;
using MailClient.views;

namespace MailClient.viewmodels;

public class InputViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private string _userName;
    private string? _password;
    private string? _smtp;
    private string? _imap;

    public string UserName
    {
        get => _userName;
        set
        {
            _userName = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UserName)));
        }
    }

    public string? Password
    {
        get => _password;
        set
        {
            _password = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Password)));
        }
    }

    public string? Smtp
    {
        get => _smtp;
        set
        {
            _smtp = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Smtp)));
        }
    }

    public string? Imap
    {
        get => _imap;
        set
        {
            _imap = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Imap)));
        }
    }

    public async Task SendDataToContentManager()
    {
        if (Smtp != null)
        {
            await ContentManager.SaveRegistration(UserName, Password, Smtp, Imap)!;
        }
        else
        {
            await ContentManager.CheckLogin(UserName, Password)!;
        }
    }
}