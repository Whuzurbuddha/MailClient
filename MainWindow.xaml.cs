using System;
using System.Windows;
using MailClient.DataController;
using MailClient.Models;

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
            LoadedPage();
        }
        private void LoadedPage()
        {
            var getMailViewModel = new GetMailViewModel();
            getMailViewModel.GenerateMailLists();
        }
    }
}