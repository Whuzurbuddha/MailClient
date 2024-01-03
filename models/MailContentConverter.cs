using System;
using System.Globalization;
using System.Windows.Data;
using MailClient.Models;

namespace MailClient.models;

public class MailContentConverter : IValueConverter
{
    private GetMailViewModel? _getMailViewModel;
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        _getMailViewModel = new GetMailViewModel();
        if (_getMailViewModel.SelectedMailText != null)
        {
            Console.WriteLine(_getMailViewModel.SelectedMailText);
            return _getMailViewModel.SelectedMailText;
        }
        else
        {
             return null;
        }
    }
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}