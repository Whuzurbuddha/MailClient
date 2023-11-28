using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using MailClient.viewmodels;

namespace MailClient.views;

public partial class SendMailView : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private Task<string?> _result;

    public Task<string?> Result
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
        Result = sendMailViewModel.SendMail();
    }
    
    private void SendMailToViewModel(object sender, RoutedEventArgs e)
    {
        Console.WriteLine(Result);
    }
}