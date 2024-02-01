using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MailClient.DataController;

public abstract class ReadMailCache
{
    private static ObservableCollection<UserContent>? _userAccounts;
    private static ObservableCollection<MailItem>? _mailBox;
    
    
    public static async Task<ObservableCollection<UserContent>?> GetLoadedMails()
    {
        _userAccounts = new ObservableCollection<UserContent>();
        var mainDirectory = ConstPaths.MainDirectory;
        var mailAccounts = Directory.GetDirectories($@"{mainDirectory}\\mailaccounts");

        if (mailAccounts.Length == 0) return null;

        for (var i = 0; i < mailAccounts.Length; i++)
        {
            var account = mailAccounts[i];
            
            var lastDot = account.LastIndexOf('\\');
            var accountName = lastDot < 0 ? "" : account.Substring((lastDot+1)!).ToLower();
            _mailBox = new ObservableCollection<MailItem>(); 
            
            var mailList = Directory.GetDirectories($@"{account}\Temp\");
            if(!mailList.Any()) continue;
            foreach (var mail in mailList)
            {
                try
                {
                    var mailFile = Directory.GetFiles(mail, "*.json");
                    if (!mailFile.Any()) continue;
                    using var reader = new StreamReader(mailFile[0]);
                    var jsonContent = await reader.ReadToEndAsync();
                    var result = JsonSerializer.Deserialize<MailItem>(jsonContent);
                    var mailItem = new MailItem()
                    {
                        Date = result?.Date,
                        MessageId = result?.MessageId,
                        MessageSubject = result?.MessageSubject,
                        MessageSender = result?.MessageSender,
                        MessageText = result?.MessageText,
                        AttachmentList = result?.AttachmentList,
                        HasAttachment = result?.HasAttachment,
                        AttachmentPath = result?.AttachmentPath
                    };
                    _mailBox.Add(mailItem);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            var userContent = new UserContent
            {
                AccountName = accountName,
                Mailbox = _mailBox
            };
            
            _userAccounts.Add(userContent);
        }
        return _userAccounts;
    }
    public class UserContent
    {
        public string? AccountName { get; set; }
        public ObservableCollection<MailItem>? Mailbox { get; set; }
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