using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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
        var tempDirectory = $@"{ConstPaths.MailAccounts!}\{accountName}\Temp\";
        var newId = messageId?.Replace('$', ' ').Replace(" ", "");
        
        if (!Directory.Exists(tempDirectory)) CreateDirectory(tempDirectory);
        
        var newSubdirectory = $@"{tempDirectory}{newId}";
        if (!Directory.Exists(newSubdirectory)) CreateDirectory(newSubdirectory);
        
        var attachmentDir = $@"{tempDirectory}\{newId}\attachment";
        if (!Directory.Exists(attachmentDir)) CreateDirectory(attachmentDir);
        
        foreach (var  attachment  in attachments)
        {
            try
            {
                var fileName = attachment.ContentType.Name.Replace(" ", "");
                var newFilePath = $@"{attachmentDir}\{fileName}";
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