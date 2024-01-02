using System;
using System.Globalization;
using System.Windows.Data;
using static FontAwesome.WPF.FontAwesomeIcon;

namespace MailClient.models
{
    public class ToIconConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var fileEnding = value?.ToString()!.Split(".")[1];
            return fileEnding switch
            {
                "png" or "jpeg" or "jpg" or "bmp" or "svg" or "tif" or "svg"=> FilePictureOutline,
                "pdf" => FilePdfOutline,
                "word" or "doc" or "docx" => FileWordOutline,
                "xlsx" or "xlsm" or "xltx" or "xls" => FileExcelOutline,
                "txt" or "odt" or "md"=> FileTextOutline,
                "zip" or "tar.gz" or "gzip" or "tar" or "7z" or "rar" or "jar" => FileZipOutline,
                "mp3" or "mid" or "wav" or "wave" or "ogg" or "flac" => FileAudioOutline,
                "avi" or "flv" or "mov" or "mp4" => FileVideoOutline,
                _ => false
            };
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}