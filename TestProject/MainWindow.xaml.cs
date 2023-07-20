using System.IO;
using System.Windows;
using TestProject.Pages;

namespace TestProject
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.NavigationService.Navigate(new TasksPage());

        }

    }
}
