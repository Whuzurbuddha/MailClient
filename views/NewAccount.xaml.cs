using System.ComponentModel;
using System.Windows;
using MailClient.DataController;

namespace MailClient.views
{
    /// <summary>
    /// Interaktionslogik für NewAccount.xaml
    /// </summary>
    public partial class NewAccount : Window, INotifyPropertyChanged
    {
        public NewAccount() 
        {
            InitializeComponent();
            DataContext = this;
        }
        private async void SaveNewAccount(object sender, RoutedEventArgs e)
        {
            var saveAccountResult = await SaveAccountData.SaveMailAccount(AccountName, UserMail, Password, Smtp, Imap);
            if(saveAccountResult) Close();
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        private string? _accountName;
        private string? _userMail;
        private string? _password;
        private string? _smpt;
        private string? _imap;
        public string? AccountName
        {
            get => _accountName;
            set
            {
                _accountName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AccountName)));
            }
        }
        public string? UserMail{
            get => _userMail;
            set
            {
                _userMail = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UserMail)));
            }
                
        }

        public string? Password
        {
            get => _password;
            set
            {
                _password = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Password)));
            }
        }
        public string? Smtp
        {
            get => _smpt;
            set
            {
                _smpt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Smtp)));
            }
        }

        public string? Imap
        {
            get => _imap;
            set
            {
                _imap = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Imap)));
            }
        }
    }
}
