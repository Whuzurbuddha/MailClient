﻿using System;
using System.ComponentModel;
using System.Threading.Tasks;
using MailClient.DataController;

namespace MailClient.viewmodels;

public class SendMailViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private string? _recipient;
    private string? _regarding;
    private string? _mailContent;

    public string? Recipient
    {
        get => _recipient;
        set
        {
            _recipient = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Recipient)));
        }
    }

    public string? Regarding
    {
        get => _regarding;
        set
        {
            _regarding = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Regarding)));
        }
    }

    public string? MailContent
    {
        get => _mailContent;
        set
        {
            _mailContent = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MailContent)));
        }
    }

    private bool _mailStatus;
    public async Task<string?> SendMail()
    {
        _mailStatus = await EmailController.SendingMail(Recipient, Regarding, MailContent);
        return _mailContent;
    }
}