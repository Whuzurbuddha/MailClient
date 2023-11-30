using System.Collections.ObjectModel;
using MailClient.DataController;

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

    private void ReceivedMailsOverview()
    {
        EmailController.ReceivingMail();
        /*var subjectsList = EmailController.ReceivingMail();
        foreach (var subject in subjectsList)
        {
            ReceivedMailOverview.Items.Add(subject);
        }*/
    }
}