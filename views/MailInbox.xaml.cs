using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MailClient.DataController;
using MailClient.models;
using MailClient.Models;
using MailClient.viewmodels;
using MailKit;
using MailKit.Net.Imap;
using MimeKit;

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
        //LoadMailAccounts();
    }
    
    private void SetSender(object sender, RoutedEventArgs e)
    {
        if (sender is not DataGridRow { IsSelected: true } dataGridRow) return;
        if (dataGridRow.DataContext is EmailController.MailItem mailItem)
        {
            (DataContext as GetMailViewModel)?.SetSelectedMailText(mailItem.MessageText, mailItem.MessageSender, mailItem.AttachmentList);
        }
    }

    private FileView _fileView;
    private void SetFile(object sender, RoutedEventArgs e)
    {
        if (sender is not ComboBoxItem { IsSelected: true } comboBoxItem) return;
        if (comboBoxItem.DataContext is EmailController.AttachmentListitem boxItem)
        {
            _fileView = new FileView();
            _fileView.Show();
            if (boxItem.AtthachmentFilePath == null) return;
            _fileView.OpenFile($"file:///{boxItem.AtthachmentFilePath}");
        }
    }
    private void LoadMailAccounts()
    {
        (DataContext as GetMailViewModel)?.GenerateAccountOverview();
    }
}
