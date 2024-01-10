using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using MailClient.DataController;
using MailClient.views;
using MimeKit;

namespace MailClient.Models
{
    public class GetMailViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        // ReSharper disable once UnassignedField.Global
        private EmailController? _controller = new EmailController();
        
        private string? _selectedMailText;
        private string? _selectedMailSender;
        private ObservableCollection<EmailController.MailItem>? _mailBox;
        private ObservableCollection<EmailController.AttachmentListitem>? _selectedMailAttachmentList;
        private string? _selectedFilePath;
        private ObservableCollection<EmailController.MailItem>? _selectedMailbox;
        private ObservableCollection<ReadMailAccountJSON.UserContent>? _userAccounts;

        public ObservableCollection<EmailController.MailItem>? MailBox
        {
            get => _mailBox;
            set
            {
                _mailBox = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MailBox)));
            }
        }
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

        public string? SelectedFilePath
        {
            get => _selectedFilePath;
            set
            {
                _selectedFilePath = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedFilePath)));
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
            if(attachmentList == null) return;
            SelectedMailAttachmentList = attachmentList;
        }

        public void SetMailBoxSelection(ObservableCollection<EmailController.MailItem>? mailBox)
        {
            SelectedMailBox = mailBox;
        }
        public void SetSelectedMailAttachmentFilePath(string? filePath)
        {
            SelectedFilePath = filePath;
        }
    }
}

