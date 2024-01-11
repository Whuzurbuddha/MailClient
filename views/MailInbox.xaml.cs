using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using MailClient.DataController;
using MailClient.models;
using MailClient.Models;
using MailClient.viewmodels;
using MailKit;
using MailKit.Net.Imap;
using MimeKit;

namespace MailClient.views;

public partial class MailInbox
{
    public MailInbox()
    {
        InitializeComponent();
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

    public void SetMailbox(ObservableCollection<EmailController.MailItem>? mailBox)
    {
        foreach (var mail in mailBox)
        {
            ReceivedMailOverview.Items.Remove(mail);
            ReceivedMailOverview.Items.Add(mail);
            ReceivedMailOverview.Items.Refresh();
        }
    }
}
