using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
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
        private ObservableCollection<SelectedMailAttachment>? _selectedMailAttachmentsList;
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

        public ObservableCollection<SelectedMailAttachment>? SelectedMailAttachmentsList
        {
            get => _selectedMailAttachmentsList;
            set
            {
                _selectedMailAttachmentsList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedMailAttachmentsList)));
            }
        }

        public async Task<ObservableCollection<EmailController.MailItem>?> GenerateMailLists()
        {
            _mailBox = new ObservableCollection<EmailController.MailItem>();
            _controller = new EmailController();
             MailBox = await _controller.ReceivingMailAsync();
             if (MailBox?.ToString() != string.Empty) UserPage.CloseLoading(); ;
                return MailBox;
        }
        public void SetSelectedMailText(string? mailText, string? mailSender,  IEnumerable<MimeEntity>? attachments)
        {
            SelectedMailText = mailText;
            SelectedMailSender = mailSender;
            SelectedMailAttachments = attachments;

            _selectedMailAttachmentsList = new ObservableCollection<SelectedMailAttachment>();

            if (SelectedMailAttachments != null)
                foreach (var attachment in SelectedMailAttachments)
                {
                    var selectedMailAttachment = new SelectedMailAttachment
                    {
                        ContentType = attachment.ContentType.ToString()
                    };
                    _selectedMailAttachmentsList.Add(selectedMailAttachment);
                }

            SelectedMailAttachmentsList = _selectedMailAttachmentsList;
        }

        public class SelectedMailAttachment
        {
            public string? ContentType { get; set; }
        }
        public void SetDownloadContent(IEnumerable<MimeEntity>? attachments)
        {
            Console.WriteLine(attachments);
        }
    }
}
