using System;
using System.Windows;
using MailClient.DataController;
using MailClient.views;

namespace MailClient.models;

public static class LoginModel
{
    public static void CalculateCache()
    {
        var userWindow = new UserPage();
        var userCache = ReadCache.ReadUserCache();
        if (userCache == null) return;
        var past = userCache.Date;
        var now = DateTime.Now;
        var timeSpan = ((now - past)!).Value.Hours;
        if (timeSpan > 12) return;
        userWindow.Show();
        Application.Current.MainWindow?.Close();
    }
}