using System;
using System.ComponentModel;

namespace MailClient.models;

public class CalendarModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private string? _currentDate;
    public string? CurrentDate
    {
        get => _currentDate;
        set
        {
            _currentDate = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentDate)));
        }
    }

    public void GenerateCurrentDate()
    {
        var currentDay = DateTime.Today.Day;
        var currentMonth = DateTime.Today.Month;
        var currentYear = DateTime.Today.Year;
        var currentDate = $"{currentDay}.{currentMonth}.{currentYear}";
        CurrentDate = currentDate;
    }
}