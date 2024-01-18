using System;
using System.IO;
using System.Text.Json;
using System.Windows;
using Microsoft.VisualBasic;

namespace MailClient.DataController
{
    public class ReadCache
    {
        private static string? _filePath = ConstPaths.LoginChachePath;
        public string? Date { get; set; }

        public class Cache
        {
            public DateTime? Date { get; init; }
        }
       public static Cache? ReadUserCache()
       {
           if (File.Exists(_filePath))
           {
               try
               {
                   string jsonContent;
                   using (var reader = new StreamReader(_filePath!))
                   {
                       jsonContent = reader.ReadToEnd();
                   }
                   var readJson = JsonSerializer.Deserialize<ReadCache>(jsonContent);
                
                   var dateString = readJson?.Date;

                   var date = DateTime.Parse(dateString!);
                
                   var userCache = new Cache()
                   {
                       Date = date
                   };
                   return userCache;
               }
               catch (Exception e)
               {
                   MessageBox.Show(e.ToString());
                   throw;
               }
           };
           return null;
       }
    }
}