using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Data;

namespace MailClient.models;

public class IsLoadedConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if(value == (object?)true)
        {
            return Color.GreenYellow;
        }
        return Color.White;
    }
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}