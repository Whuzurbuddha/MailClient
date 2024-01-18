using System;
using System.Collections.Generic;
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
        private static bool? ValidAttachment(MimeEntity attachment)
        {
            var fileName = attachment.ContentType.Name;
            var lastDot = fileName?.LastIndexOf('.');
            var fileType = lastDot < 0 ? "" : fileName?.Substring((int)(lastDot+1)!).ToLower();
            if (!string.IsNullOrEmpty(fileName))
            {
                string[] allowedTypes =
                {
                    "png", "jpg", "jpeg", "bmp", "svg", "tif", "pdf", "word", "doc", "docx", "xlsx",
                    "xlsm", "xltx", "xls", "txt", "odt", "md", "mp3", "mid", "wav", "wave", "ogg", "flac", "avi",
                    "flv",
                    "mov", "mp4",
                };
                if (allowedTypes.Contains(fileType))
                {
                    return true;
                }
            }
            return false;
        }
        
        private ObservableCollection<MailItem>? _messagesOverview;
        private ObservableCollection<AttachmentListitem>? _attachmentList; 
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
                    var messageBody = await inbox.GetMessageAsync(i);
                    var dateTime = messageBody.Date;
                    var date = dateTime.ToString().Split(" ")[0];
                    var message = new MailItem
                    {
                        Date = date,
                        MessageId = messageBody.MessageId,
                        MessageSubject = messageBody.Subject,
                        MessageSender = messageBody.From.ToString(),
                        MessageText = messageBody.TextBody,
                    };
                    _attachmentList = new ObservableCollection<AttachmentListitem>();
                    
                    var attachments = messageBody.Attachments.ToArray();
                    if (attachments.Any())
                    {
                        var newAttachmentList = new List<MimeEntity>();
                        var nonevalidAttachmentsList = new List<MimeEntity>();
                        
                        foreach (var attachment in attachments)
                        {
                            var validAttachment = ValidAttachment(attachment);
                            if (validAttachment == true)
                            {
                                newAttachmentList.Add(attachment);
                            }
                            else
                            {
                                nonevalidAttachmentsList.Add(attachment);
                            }
                        }
                        message.HasAttachment = true;

                        if (nonevalidAttachmentsList.Count > 0)
                        {
                            foreach (var nonevalid in nonevalidAttachmentsList)
                            {
                                if (nonevalid.ContentType.Name != "smime.p7s")
                                {
                                    var attachmentPathListitem = new AttachmentListitem
                                    {
                                        IsLoaded = false
                                    };
                                
                                    if (!string.IsNullOrEmpty(nonevalid.ContentType.Name))
                                    {
                                        attachmentPathListitem.AttachmentFileName =
                                            nonevalid.ContentType.Name.Replace(" ", "");
                                    }
                                    else
                                    {
                                        attachmentPathListitem.AttachmentFileName = "unknown filename";
                                    }
                                    _attachmentList.Add(attachmentPathListitem);
                                }
                            }
                        }
                        
                        if (newAttachmentList.Count > 0)
                        {
                           var subDirectory = await NewAttachmentCache(accountName, message.MessageId, newAttachmentList, messageBody.BodyParts)!;
                           message.AttachmentPath = subDirectory;

                           foreach (var cleandAttachment in newAttachmentList)
                           {
                               var fileName = "";
                               if (!string.IsNullOrEmpty(cleandAttachment.ContentType.Name))
                               {
                                   fileName = cleandAttachment.ContentType.Name;
                               }
                               else
                               {
                                   fileName = "unknown filename";
                               }
                               var attachmentPathListitem = new AttachmentListitem
                               {
                                   AttachmentFileName = fileName,
                                   AttachmentFileType = cleandAttachment.ContentType.MediaSubtype,
                                   AtthachmentFilePath = $"{subDirectory}{cleandAttachment.ContentType.Name.Replace(" ", "")}",
                                   IsLoaded = true
                               };
                               _attachmentList.Add(attachmentPathListitem);
                           }
                        };
                        message.AttachmentList = _attachmentList;
                    }
                    _messagesOverview.Add(message);
                }
                
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
            public string? Date { get; set; }
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
            public string? AttachmentFileName { get; set; }
            public string? AttachmentFileType { get; set; }
            public string? AtthachmentFilePath { get; init; }
            public bool? IsLoaded { get; set; }
        }
    }
}
