using System.Windows;
using MailClient.viewmodels;

namespace MailClient.views
{
    /// <summary>
    /// Interaktionslogik für AddressBook.xaml
    /// </summary>
    public partial class AddressBook : Window
    {
        public AddressBook()
        {
            InitializeComponent();
            var contactViewModel = new ContactViewModel();
            DataContext = contactViewModel;
        }
    }
}
