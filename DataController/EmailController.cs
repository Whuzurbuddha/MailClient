using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MailKit;
using MailKit.Net.Imap;
using MimeKit;
using static MailClient.DataController.AttachmentCache;

namespace MailClient.DataController
{
    public class EmailController
    {
        private ObservableCollection<MailItem>? _messagesOverview;
        private ObservableCollection<AttachmentListitem>? _attachmentList; 
        private AttachmentListitem? _attachmentPathListitem;
        public async Task<ObservableCollection<MailItem>?> ReceivingMailAsync(string  accountName, string? imap, string? userMail, string? password)
        {
            _messagesOverview = new ObservableCollection<MailItem>();
            using var client = new ImapClient();

            try
            {
                await client.ConnectAsync(imap, 993, true);
                await client.AuthenticateAsync(userMail, password);
                var inbox = client.Inbox;
                await inbox.OpenAsync(FolderAccess.ReadOnly);
                for (var i = 0; i < inbox.Count; i++)               //single message content =>
                {
                    if (i < 2)
                    {
                        Console.WriteLine($"FOUND {i + 1} MAIL");
                    }
                    else
                    {
                        Console.WriteLine($"FOUND {i + 1} MAILS");
                    }
                    var messageBody = await inbox.GetMessageAsync(i);
                    var message = new MailItem
                    {
                        MessageId = messageBody.MessageId,
                        MessageSubject = messageBody.Subject,
                        MessageSender = messageBody.From.ToString(),
                        MessageText = messageBody.TextBody,
                    };
                    _attachmentList = new ObservableCollection<AttachmentListitem>();
                    
                    var mimeEntities = messageBody.Attachments;
                    var attachments = messageBody.Attachments as MimeEntity[] ?? mimeEntities.ToArray();
                    if (attachments.Any())
                    {
                        message.HasAttachment = true;
                        
                        var subDirectory =  await NewAttachmentCache(accountName, message.MessageId, attachments, messageBody.BodyParts)!;
                        message.AttachmentPath = subDirectory;

                        foreach (var attachment in attachments)
                        {
                            _attachmentPathListitem = new AttachmentListitem
                            {
                                AttachmentFileName = attachment.ContentType.Name,
                                AttachmentFileType = attachment.ContentType.MediaSubtype,
                                AtthachmentFilePath = $"{subDirectory}{attachment.ContentType.Name}"
                            };
                            
                            _attachmentList?.Add(_attachmentPathListitem);
                        }
                        message.AttachmentList = _attachmentList;
                    }
                    _messagesOverview.Add(message);
                }
                Console.WriteLine("DISCONNECT SERVER");
                await client.DisconnectAsync(true);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return _messagesOverview ?? null;
        }
        
        public class MailItem
        {
            public string? MessageId { get; init; }
            public string? MessageSubject { get; init; }
            public string? MessageSender { get; init; }
            public string? MessageText { get; init; }
            public ObservableCollection<AttachmentListitem>? AttachmentList { get; set; }
            public bool? HasAttachment { get; set; }
            public string? AttachmentPath { get; set; }
        }

        public class AttachmentListitem
        {
            public string? AttachmentFileName { get; init; }
            public string? AttachmentFileType { get; set; }
            public string? AtthachmentFilePath { get; init; }
        }
    }
}