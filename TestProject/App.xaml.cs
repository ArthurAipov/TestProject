using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using TestProject.Models;

namespace TestProject
{
    public partial class App : Application
    {



        public static List<Status> statuses = new List<Status>()
        {
            new Status{id = 1, color = new SolidColorBrush(Color.FromArgb(100, 0, 255, 0)), name ="Completed"},
            new Status{id = 2, color = new SolidColorBrush(Color.FromArgb(100, 255, 0, 0)), name ="Jeopardy"},
            new Status{id = 3, color = new SolidColorBrush(Color.FromArgb(100, 255, 255, 0)), name ="Pending"},
            new Status{id = 4, name="Other"}
        };
    }
}
