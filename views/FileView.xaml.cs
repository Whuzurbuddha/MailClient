using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;

namespace MailClient.views;

public partial class FileView : Window
{

    public FileView()
    {
        InitializeComponent();
    }

    private Uri _uri;
    public void OpenFile(string?  path)
    {
        if (path == null) return;
        _uri = new Uri(path);
        WebView.Source = _uri;
    }
}