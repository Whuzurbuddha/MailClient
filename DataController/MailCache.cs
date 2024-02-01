using System;
using System.IO;
using System.Text.Json;
using MailClient.views;

namespace MailClient.DataController;

public static class MailCache
{
    private static UserPage? _userPage;
    private static System.IO.DirectoryInfo? CreateDirectory (string path)
    {
        Directory.CreateDirectory(path);
        return null;
    }
    public static async void WriteMailCache(EmailController.MailItem? message, string? accountName)
    {
        var messageId = message?.MessageId?.Replace('$', ' ').Replace(" ", "");
        var tempDirectory = $@"{ConstPaths.MailAccounts!}\{accountName}\Temp";
        if (!Directory.Exists(tempDirectory)) CreateDirectory(tempDirectory);
        
        var newMessagePath = $@"{tempDirectory}\{messageId}";
        if (Directory.Exists(newMessagePath)) return;
        
        CreateDirectory(newMessagePath);
        var newMessage = $@"{newMessagePath}\{message?.MessageSender}.json";
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
        //_userPage?.LoadMailOverview();
    }
}