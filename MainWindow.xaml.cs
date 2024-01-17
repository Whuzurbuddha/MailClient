using System.Windows;
using MailClient.models;

namespace MailClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoginModel.CalculateCache();
        }
    }
}