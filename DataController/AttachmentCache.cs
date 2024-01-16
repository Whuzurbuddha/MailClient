using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using MimeKit;

namespace MailClient.DataController;

public static class AttachmentCache
{
    private static System.IO.DirectoryInfo? CreateDirectory (string path)
    {
        Directory.CreateDirectory(path);
        return null;
    }
    
    public static async Task<string>? NewAttachmentCache(string? accountName, string? messageId, List<MimeEntity> attachments, IEnumerable<MimeEntity> bodyParts)
    {
        var documentDirectory = SpecialDirectories.MyDocuments;
        string[] user = Directory.GetDirectories($"{documentDirectory}\\MailClient");
        var tempDirectory = new StringBuilder($"{user[0]}\\{accountName}\\Temp\\");
        string[] toReplace = { "$", "#" };
        var newId = messageId;
        foreach (var c in toReplace)
        {
            newId = messageId?.Replace(c, "");  
        }
        var newSubdirectory = tempDirectory.AppendFormat($"{newId}\\").ToString();
        if (!Directory.Exists(newSubdirectory)) CreateDirectory(newSubdirectory);
            
        foreach (var  attachment  in attachments)
        {
            try
            {
                var fileName = attachment.ContentType.Name.Replace(" ", "");
                var newFilePath = $"{newSubdirectory}{fileName}";
                Console.WriteLine($"NEW FILEPATH: {newFilePath}");
                await using var stream = File.Create(newFilePath);
                if (attachment is MimePart part)
                {
                    await part.Content.DecodeToAsync(stream);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        return newSubdirectory;
    }
    //private static void DeleteCachedFile(){}
}