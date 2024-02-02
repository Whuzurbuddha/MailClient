using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows;
using MailClient.viewmodels;

namespace MailClient.DataController;

public static class EmailSender
{
    public static Task<bool> SendingMail(string?  mailSender, string? recipient, string? regarding, string? mailContent, string? filePath)
    {
        MessageBox.Show(mailSender);
        /*var fileType = new List<string>()
        {
            "pdf",
            "doc",
            "jpeg",
            "png"
        };

        if (fileType.Any(fileEnd => !string.IsNullOrEmpty(filePath) &&
                                    !filePath.EndsWith($".{fileEnd}", StringComparison.OrdinalIgnoreCase)))
        {
            MessageBox.Show("Ungültiges Dateiformat!\r\n Erlaubt sind PDF, DOC, PNG, JPEG");
            return Task.FromResult(false);
        }

        var userContent = ReadMailAccountJson.GetUserContent();
        var userMail = MailContentViewModel;
        var smtp = userContent?.;
        var encryptedPasswd = userContent?.;
        var password = ContentManager.DecryptedPasswd(encryptedPasswd);

        if (userMail == null || recipient == null)
            return Task.FromResult(true);

        var message = new MailMessage(userMail, recipient, regarding, mailContent);

        var smtpClient = new SmtpClient(smtp)
        {
            Port = 587,
            Credentials = new NetworkCredential(userMail, password),
            EnableSsl = true
        };

        if (!string.IsNullOrEmpty(filePath))
        {
            var attachment = new Attachment(filePath);
            message.Attachments.Add(attachment);
        }

        smtpClient.Send(message);
        message.Dispose();*/
        return Task.FromResult(true);
    }
}