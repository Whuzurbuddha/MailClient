using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.InteropServices.JavaScript;
using System.Windows;
using System.Windows.Controls;
using MailClient.DataController;
using MailClient.Models;

namespace MailClient.views;

public partial class MailInbox : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private string? _sender;

    public string? Sender
    {
        get => _sender;
        set
        {
            _sender = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Sender.ToString)));
        }
    }
    public MailInbox()
    {
        InitializeComponent();
        (DataContext as GetMailViewModel)?.GenerateMailLists();
    }
    
    private void SetSender(object sender, RoutedEventArgs e)
    {
        if (sender is DataGridRow dataGridRow && dataGridRow.IsSelected)
        {
            if (dataGridRow.DataContext is EmailController.MailItem mailItem)
            {
                (DataContext as GetMailViewModel)?.SetSelectedMailText(mailItem.MessageText, mailItem.MessageSender);
            }
        }
    }
}
