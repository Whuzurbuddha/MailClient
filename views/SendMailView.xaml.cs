using System.ComponentModel;
using System.IO;
using System.Windows;
using MailClient.viewmodels;

namespace MailClient.views;

public partial class SendMailView
{
    
    
    private readonly SendMailViewModel? _sendMailViewModel;
    private string? _mailSendingStatus;
    public SendMailView()
    {
        InitializeComponent();
        _sendMailViewModel = new SendMailViewModel();
        DataContext = _sendMailViewModel;
    }
    private async void SendMailToViewModel(object sender, RoutedEventArgs e)
    {
        _mailSendingStatus = await _sendMailViewModel?.SendMail()!;
        if (_mailSendingStatus != null &&_mailSendingStatus.Contains("erfolgreich"))
        {
            Close();
        }
        else
        {
            MessageBox.Show($"{_mailSendingStatus}");
        }
    }

    public class Files  : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private string? _filePath;
        public string? FilePath
        {
            get => _filePath;
            set
            {
                _filePath = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(FilePath));
            }
        }
    }

    public Files File;
    private void ShowFileLoader(object sender, RoutedEventArgs e)
    {
        File.FilePath = LoadFile.OpenDirectory();
    }
}