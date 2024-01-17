using System;
using System.Globalization;
using System.IO;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MailClient.DataController;

public class LoginCache
{
    private class CacheData
    {
        public string? Date { get; set; }
    }
    
    public static async void WriteLoginCache(string? userName)
    {
        var userDir = ConstPaths.LoginChachePath;

        var today = DateTime.Now.ToString(CultureInfo.CurrentCulture);
        var cache = new CacheData
        {
            Date = today
        };

        var json = JsonSerializer.Serialize(cache);

        await using var writer = new StreamWriter(userDir!);
        await writer.WriteAsync(json);
        writer.Close();
    }
}