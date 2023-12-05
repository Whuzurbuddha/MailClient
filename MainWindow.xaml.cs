using System.Windows;
using MailClient.DataController;
using MailClient.models;
using MailClient.views;

namespace MailClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var mailListBox = new MailBoxModel();
            mailListBox.LoadMailList();
            InitializeComponent();
        }
    }
}