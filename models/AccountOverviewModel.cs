using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using MailClient.DataController;

namespace MailClient.models;

public class AccountOverviewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private ObservableCollection<ReadMailAccountJSON.UserContent>? _userAccounts;
    public ObservableCollection<ReadMailAccountJSON.UserContent>? UserAccounts
    {
        get => _userAccounts;
        set
        {
            _userAccounts = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UserAccounts)));
        }
    }

    public async Task GenerateAccountOverview()
    {
        UserAccounts = await ReadMailAccountJSON.GetUserContent();
    }
}