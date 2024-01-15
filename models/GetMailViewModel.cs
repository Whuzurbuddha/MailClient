using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using MailClient.DataController;
using MailClient.views;

namespace MailClient.Models
{
    public class GetMailViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        
        private string? _selectedMailText;
        private string? _selectedMailSender;
        private ObservableCollection<EmailController.AttachmentListitem>? _selectedMailAttachmentList;
        private ObservableCollection<EmailController.MailItem>? _selectedMailbox;
        private ObservableCollection<ReadMailAccountJSON.UserContent>? _userAccounts;
        
        public string? SelectedMailText
        {
            get => _selectedMailText;
            set
            {
                _selectedMailText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedMailText)));
            }
        }
        public string? SelectedMailSender
        {
            get => _selectedMailSender;
            set
            {
                _selectedMailSender = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedMailSender)));
            }
        }

        public ObservableCollection<EmailController.AttachmentListitem>? SelectedMailAttachmentList
        {
            get => _selectedMailAttachmentList;
            set
            {
                _selectedMailAttachmentList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedMailAttachmentList)));
            }
        }
        public ObservableCollection<ReadMailAccountJSON.UserContent>? UserAccounts
        {
            get => _userAccounts;
            set
            {
                _userAccounts = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UserAccounts)));
            }
        }

        public ObservableCollection<EmailController.MailItem>? SelectedMailBox
        {
            get => _selectedMailbox;
            set
            {
                _selectedMailbox = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedMailBox)));
            }
        }

        public async Task GenerateAccountOverview()
        {
            Console.WriteLine("STARTED RECEIVING MAILS\r\n");
            UserAccounts = await ReadMailAccountJSON.GetUserContent();
            Console.WriteLine("FINISHED RECEIVING MAILS");
            if (UserAccounts == null) return;
            UserPage.CloseLoading();
        }
        public void SetSelectedMailText(string? mailText, string? mailSender, ObservableCollection<EmailController.AttachmentListitem>? attachmentList)
        {
            SelectedMailText = mailText;
            SelectedMailSender = mailSender;
            SelectedMailAttachmentList = attachmentList;
        }

        public void SetMailBoxSelection(ObservableCollection<EmailController.MailItem>? mailBox)
        {
            if (mailBox == null) return;
            SelectedMailBox = mailBox;
        }
    }
}
