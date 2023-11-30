using System;
using System.Collections.ObjectModel;
using MailClient.DataController;
using MimeKit;

namespace MailClient.views;

public partial class MailInbox
{
    public MailInbox()
    {
        InitializeComponent();
        ReceivedMailsOverview();
    }

    public class MailList : ObservableCollection<ReceivedMail>
    {
        public MailList() : base()
        {
            
        }
    }
    public class ReceivedMail 
    {
        public string? Sender { get; set; }
        public string? Regarding { get; set; }
        public string? Message { get; set; }        
    }
    public class Message
    {
        public string? MessageSubject { get; set; }
        public InternetAddressList? MessageSender { get; set; }
        public string? MessageId { get; set; }
        public string? MessageText { get; set; }
    }

    private void ReceivedMailsOverview()
    {
        var mailList = EmailController.ReceivingMail();
        var mails = mailList.MessageOverview;
        if (mails == null) return;
        foreach (var mail in mails)
        {
            ReceivedMailOverview.Items.Add(mail.MessageSubject);
        }
    }
}