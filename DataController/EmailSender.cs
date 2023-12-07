using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MailClient.DataController;

public class EmailSender
{
    public static async Task<bool> SendingMail(string? recipient, string? regarding, string? mailContent, string? filePath)
    {
        var userContent = ReadJson.GetUserContent();
        var userMail = userContent.User;
        var smtp = userContent.Smtp;
        var encryptedPasswd = userContent.EncryptedPasswd;
        var password = ContentManager.DecryptedPasswd(encryptedPasswd);
        var smtpClient = new SmtpClient(smtp)
        {
            Port = 587, 
            Credentials = new NetworkCredential(userMail, password),
            EnableSsl = true
        };
        if (userMail == null || recipient == null || mailContent == null) return false;
        smtpClient.Send(userMail, recipient, regarding, mailContent);
        return true;
    }
}