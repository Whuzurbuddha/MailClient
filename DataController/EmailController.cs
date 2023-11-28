using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MailClient.DataController;
//SMTP smtp.web.de
//IMAP imap.web.de
public class EmailController
{

    public static async void ServerConnect(string? password)
    {
        var serverContent = ReadJson.GetServerContent();
        var userName = serverContent.User;
        var smtp = serverContent.Smtp;

        var smtpClient = new SmtpClient(smtp)
        {
            Port = 587,
            Credentials = new NetworkCredential(userName, password),
            EnableSsl = true
        };
    }
    
    public static async Task<bool> SendingMail(string? recipient, string? regarding, string? mailContent)
    {
        return true;
    }
}