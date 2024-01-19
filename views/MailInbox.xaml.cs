using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using MailClient.DataController;
using MailClient.Models;

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
        if (dataGridRow.DataContext is ReadMailCache.MailItem mailItem)
        {
            (DataContext as GetMailViewModel)?.SetSelectedMailText(mailItem.MessageText, mailItem.MessageSender, mailItem.AttachmentList);
            if (mailItem.HasAttachment == true)
            {
                AttachmentList.Visibility = Visibility.Visible;
                AttachmentList.Items.Refresh();
            }
            else
            {
                AttachmentList.Visibility = Visibility.Collapsed;
                AttachmentList.Items.Refresh();
            }
        }
    }

    private FileView _fileView;
    private void SetFile(object sender, RoutedEventArgs e)
    {
        if (sender is not ComboBoxItem { IsSelected: true } comboBoxItem) return;
        if (comboBoxItem.DataContext is ReadMailCache.AttachmentListitem boxItem)
        {
            _fileView = new FileView();
            _fileView.Show();
            if (boxItem.AtthachmentFilePath == null) return;
            _fileView.OpenFile($"file:///{boxItem.AtthachmentFilePath}");
        }
    }

    public void SetMailbox(ObservableCollection<ReadMailCache.MailItem>? mailBox)
    {
        (DataContext as GetMailViewModel)?.SetMailBoxSelection(mailBox);
        ReceivedMailOverview.Items.Refresh();
        foreach (var mail in mailBox)
        {
            if (mail.AttachmentList == null) return;
            foreach (var attachment in mail.AttachmentList)
            {
                Console.WriteLine($"DOWNLOADPFAD: {attachment.AtthachmentFilePath}");
            }
        }
    }
}