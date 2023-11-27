using System.ComponentModel;
using System.Windows;
using MailClient.viewmodels;

namespace MailClient.views;

public partial class SendMailView : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private bool _result;

    public bool Result
    {
        get => _result;
        set
        {
            _result = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Result)));
        }
    }
    public SendMailView()
    {
        InitializeComponent();
        var sendMailViewModel = new SendMailViewModel();
        DataContext = sendMailViewModel;
    }
    
    private async void SendMailToViewModel(object sender, RoutedEventArgs e)
    {
        Result = await SendMailViewModel.SendMail();
        if(Result) this.Close();
    }
}