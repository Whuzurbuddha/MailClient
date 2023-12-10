using System.ComponentModel;
using System.Threading.Tasks;
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
        if (sender is not DataGridRow { IsSelected: true } dataGridRow) return;
        if (dataGridRow.DataContext is EmailController.MailItem mailItem)
        {
            (DataContext as GetMailViewModel)?.SetSelectedMailText(mailItem.MessageText, mailItem.MessageSender);
        }
    }

    public async Task<bool> RefreshMailBoxView()
    {
        ReceivedMailOverview.Items.Refresh();
        return true;
    }
}
