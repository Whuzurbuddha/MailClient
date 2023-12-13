using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        Loaded += LoadMailList;
    }

    private void LoadMailList(object sender, RoutedEventArgs routedEventArgs)
    {
        (DataContext as GetMailViewModel)?.GenerateMailLists();
    }
    
    private void SetSender(object sender, RoutedEventArgs e)
    {
        if (sender is not DataGridRow { IsSelected: true } dataGridRow) return;
        if (dataGridRow.DataContext is EmailController.MailItem mailItem)
        {
           (DataContext as GetMailViewModel)?.SetSelectedMailText(mailItem.MessageText, mailItem.MessageSender,  mailItem.Attachments);
        }
    }

    private void Download(object sender, RoutedEventArgs e)
    {
        /*if (sender is not ComboBoxItem { IsSelected: true } comboBoxItem) return;
        if (comboBoxItem.DataContext is EmailController.MailItem comboItem)
        {
        }*/
    }
}
