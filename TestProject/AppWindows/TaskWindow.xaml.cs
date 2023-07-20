using System.Windows;

namespace TestProject.AppWindows
{
    public partial class TaskWindow : Window
    {
        public TaskWindow(Models.Task task)
        {
            InitializeComponent();
            DataContext = task;
        }
    }
}
