using MailClient.DataController;

namespace MailClient.viewmodels;

public class ReceivingMailViewModel
{
    private void GetMailOverview()
    {
        var inbox = EmailController.ReceivingMail();
    }
}