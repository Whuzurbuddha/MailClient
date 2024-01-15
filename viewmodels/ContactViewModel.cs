using System.ComponentModel;

namespace MailClient.viewmodels
{
    class ContactViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string? _forename;
        private string? _surname;
        private string? _email;
        private string? _phone;
        private string? _address;
        private string? _city;
        private string? _country;
        private string? _company;

        public string? Forename
        {
            get => _forename;
            set
            {
                _forename = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Forename)));
            }
        }

        public string? Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Surname)));
            }
        }

        public string? Email
        {
            get => _email;
            set
            {
                _email = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Email)));
            }
        }

        public string? Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Phone)));
            }
        }

        public string? Address
        {
            get => _address;
            set
            {
                _address= value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Address)));
            }
        }

        public string? City
        {
            get => _city;
            set
            {
                _city = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(City)));
            }
        }

        public string? Country
        {
            get => _country;
            set
            {
                _country = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Country)));
            }
        }

        public string? Company
        {
            get => _company;
            set
            {
                _company = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Company)));
            }
        }
    }
}
