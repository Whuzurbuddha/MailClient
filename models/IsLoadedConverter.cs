using System;
using System.Globalization;
using System.Windows.Data;
using FontAwesome.WPF;
using static FontAwesome.WPF.FontAwesomeIcon;
namespace MailClient.models;

public class IsLoadedConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if(value == (object?)true)
        {
            return Download;
        }
        return Apple;
    }
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}