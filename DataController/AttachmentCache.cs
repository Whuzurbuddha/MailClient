using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MimeKit;

namespace MailClient.DataController;

public static class AttachmentCache
{
    private static System.IO.DirectoryInfo CreateDirectory (string path)
    {
        Directory.CreateDirectory(path);
        return null;
    }

    public static async Task<string>? NewAttachmentCache(string? messageId, MimeEntity[] attachments,  IEnumerable<MimeEntity> bodyParts)
    {
        var tempDirectory = new StringBuilder(@"C:\Users\alexander\RiderProjects\MailClient\Data\Temp\");
        var newSubdirectory = tempDirectory.AppendFormat($"{messageId}\\").ToString();
        try
        {
            if (!Directory.Exists(newSubdirectory)) CreateDirectory(newSubdirectory);
            
            foreach (var  attachment  in attachments)
            {
                var newFile = $"{newSubdirectory}{attachment.ContentType.Name}";
                await using var stream = File.Create(newFile);
                if (attachment is MimePart)
                {
                    await ((MimePart)attachment).Content.DecodeToAsync(stream);
                }
            } 
        }
        catch (Exception e)
        {
            MessageBox.Show(e.ToString());
            throw;
        }

        return newSubdirectory;
    }
    //private static void DeleteCachedFile(){}
}