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
        ReceivedMailOverview.Items.Remove((DataContext as GetMailViewModel)?.SelectedMailBox);
        if (mailBox == null) return;
        foreach (var mail in mailBox)
        {
            ReceivedMailOverview.Items.Refresh();
            ReceivedMailOverview.Items.Add(mail);
            ReceivedMailOverview.Items.Refresh();
        }
    }
}