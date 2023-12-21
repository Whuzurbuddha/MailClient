using System;
using System.Globalization;
using System.Windows.Data;
using FontAwesome.WPF;
namespace MailClient.Models;

public class ToIconConverter
{
    public class IconConverter : IValueConverter
    {

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null) return null;
            var end = value?.ToString()?.Split(".")[1];
            return end switch
            {
                "pdf" => FontAwesomeIcon.FilePdfOutline,
                _ => false
            };
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}