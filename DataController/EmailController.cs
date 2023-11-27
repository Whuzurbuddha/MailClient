using System;
using System.Threading.Tasks;

namespace MailClient.DataController;

public class EmailController
{
    public string? UserName { get; set; }
    public string? Smtp { get; set; }
    public string? Imap { get; set; }
    
    public static async Task<bool> SendingMail(string? recipient, string? regarding, string? mailContent)
    {
        var serverContent = ReadJson.GetServerContent();
        Console.WriteLine(serverContent);
        return true;
    }
}