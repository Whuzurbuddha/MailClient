using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
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
        private IEnumerable<MimeEntity>? _selectedMailAttachments;

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

        public IEnumerable<MimeEntity>? SelectedMailAttachments
        {
            get => _selectedMailAttachments;
            set
            {
                _selectedMailAttachments = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedMailAttachments)));
            }
        }
        
        public async Task<ObservableCollection<EmailController.MailItem>?> GenerateMailLists()
        {
            _mailBox = new ObservableCollection<EmailController.MailItem>();
            _controller = new EmailController();
             MailBox = await _controller.ReceivingMailAsync();
             if (MailBox?.ToString() != string.Empty) UserPage.CloseLoading();
                return MailBox;
        }
        public void SetSelectedMailText(string? mailText, string? mailSender, IEnumerable<MimeEntity>? attachments)
        {
            SelectedMailText = mailText;
            SelectedMailSender = mailSender;
            SelectedMailAttachments = attachments;
        }

        public void SetDownloadSelect(AttachmentCollection fileNameList)
        {
            
        }
    }
}
