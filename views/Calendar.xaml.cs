using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using MailClient.models;

namespace MailClient.views;

public partial class Calendar : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private string? _date;
    private int? _year;
    private int? _month;
    private int? _day;
    private int? _range;
    private ListViewItem? _dayPanel;

    public string? Date
    {
        get => _date;
        set
        {
            _date = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Date)));
        }
    }
    public int? Year
    {
        get => _year;
        set
        {
            _year = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Year)));
        }
    }
    public int? Month
    {
        get => _month;
        set
        {
            _month = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Month)));
        }
    }
    public int? Day
    {
        get => _day;
        set
        {
            _day = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Day)));
        }
    }

    public int? Range
    {
        get => _range;
        set
        {
            _range = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Range)));
        }
    }

    public ListViewItem? DayPanel
    {
        get => _dayPanel;
        set
        {
            _dayPanel = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DayPanel)));
        }
    }

    public Calendar()
    {
        InitializeComponent();
        Loaded += SetCurrentDate;
        (DataContext as CalendarModel)?.GenerateCurrentDate();
        GenerateCalender();
    }

    private void SetCurrentDate(object sender, RoutedEventArgs routedEventArgs)
    {
        Day = DateTime.Today.Day;
        Month = DateTime.Today.Month;
        Year = DateTime.Today.Year;
        Date = $"{Day}.{Month}.{Year}";
        SetDate();
        GenerateCalender();
    }

    private void BackDate(object sender, RoutedEventArgs routedEventArgs)
    {
        Month -= 1;
        if (Month >= 1 & Month <= 12)
        {
            if ((Month == 4 | Month == 5 | Month == 6 | Month == 9 | Month == 11) & Day > 30)
            {
                Day = 30;
            }
            if  ((Month == 2 & Year % 4 == 0) & Day > 29)
            {
                Day = 29;
            }
            else if  ((Month == 2 & Year % 4 != 0) & Day > 28)
            {
                Day = 28;
            }

            Date = $"{Day}.{Month}.{Year}";
            SetDate();
        }
        else
        {
            Year -= 1;
            Month = 12;
            Date = $"{Day}.{Month}.{Year}";
            SetDate();
        }
        GenerateCalender();
    }

    private void ForwardDate(object sender, RoutedEventArgs routedEventArgs)
    {
        Month += 1;
        if (Month >= 1 & Month <= 12)
        {
            if ((Month == 4 | Month == 5 | Month == 6 | Month == 9 | Month == 11) & Day > 30)
            {
                Day = 30;
            }
            if ((Month == 2 & Year % 4 == 0) & Day > 29)
            {
                Day = 29;
            }
            else if ((Month == 2 & Year % 4 != 0) & Day > 28)
            {
                Day = 28;
            }
            Date = $"{Day}.{Month}.{Year}";
            SetDate();
        }
        else
        {
            Year += 1;
            Month = 1;
            Date = $"{Day}.{Month}.{Year}";
            SetDate();
        }
        GenerateCalender();
    }
    private void SetDate()
    {
        CurrentDate.Text = Date;
    }

    private void GenerateCalender()
    {
        if (Month == 4 | Month == 5 | Month == 6 | Month == 9 | Month == 11)
        {
            Range = 30;
        }
        else
        {
            Range = 31;
        }
        if (Month == 2 & Year % 4 == 0)
        {
            Range = 29;
        }
        else if (Month == 2 & Year % 4 != 0)
        {
            Range = 28;
        }
        CalenderPanel.Children.Remove(DayPanel);
        CalenderPanel.Children.Clear();
        for (var i = 1; i <= Range; i++)
        {
            var date = $"{i}.{Month}.{Year}";
            DayPanel = new ListViewItem { Content = date };
            CalenderPanel.Children.Add(DayPanel);
        }
    }
}
