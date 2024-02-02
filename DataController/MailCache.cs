using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace MailClient.DataController;

public static class MailCache
{
    private static System.IO.DirectoryInfo? CreateDirectory (string path)
    {
        Directory.CreateDirectory(path);
        return null;
    }
    public static async void WriteMailCache(EmailController.MailItem? message, string? accountName)
    {
        Console.WriteLine("START WRITING MAIL JSON");
        var messageId = message?.MessageId;
        var tempDirectory = $@"{ConstPaths.MailAccounts!}\{accountName}\Temp";
        if (!Directory.Exists(tempDirectory)) CreateDirectory(tempDirectory);
        
        var newMessagePath = $@"{tempDirectory}\{messageId}";
        if (!Directory.Exists(newMessagePath)) CreateDirectory(newMessagePath);
        
        var newMessage = $@"{newMessagePath}\{message?.MessageSender}.json";
        if (File.Exists(newMessage)) return;
        try
        {
            var json = JsonSerializer.Serialize(message, new JsonSerializerOptions { WriteIndented = true });
            
            await using var writer = new StreamWriter(newMessage);
            await writer.WriteAsync(json);
            writer.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}