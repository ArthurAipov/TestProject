using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TestProject.Models;
using System.Windows.Media;
using System.Threading;
using System.Windows.Threading;

namespace TestProject.Pages
{
    public partial class TasksPage : Page
    {
        private List<Project> projects = new List<Project>();
        private List<Task> allTasks = new List<Task>();
        private Random random = new Random();
        private DispatcherTimer timer = new DispatcherTimer();
        private TextBlock textBlockLoading = new TextBlock();
        public TasksPage()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(4);
            timer.Tick += Timer_Tick;
            textBlockLoading.Text = "Loading...";
            textBlockLoading.HorizontalAlignment = HorizontalAlignment.Center;
            textBlockLoading.VerticalAlignment = VerticalAlignment.Center;
            textBlockLoading.FontSize = 22;
            Refresh();
        }
        private void Refresh()
        {
            MainGrid.RowDefinitions.Add(new RowDefinition() { MaxHeight = 100 });
            MainGrid.ColumnDefinitions.Add(new ColumnDefinition() { MinWidth = 100 });
            CreateProjects();
            CreateTasks();
            SetTextBlocks();
            CreateRows();
            CreateColumns();
            SetTasks();
        }
        private void SetTextBlocks()
        {
            var completed = allTasks.Count(x => x.status.id == 1);
            var pending = allTasks.Count(x => x.status.id == 3);
            var jeopardy = allTasks.Count(x => x.status.id == 2);
            TextBlockComlpleted.Text = $"completed: {completed}";
            TextBlockPending.Text = $"pending: {pending}";
            TextBlockJeopadry.Text = $"jeopardy: {jeopardy}";
        }
        private void CreateProjects()
        {
            projects.Clear();
            var projectsCount = random.Next(5, 100);
            for (int i = 1; i < projectsCount + 1; i++)
            {
                var project = new Project();
                project.name = $"project_{i}";
                project.id = i;
                projects.Add(project);
            }
        }
        private void CreateTasks()
        {
            allTasks.Clear();
            var statuses = App.statuses;
            foreach (var project in projects)
            {
                for (int i = 0; i < random.Next(100, 1000); i++)
                {
                    var task = new Task();
                    task.status = statuses[random.Next(0, statuses.Count)];
                    var mounth = random.Next(1, 12);
                    var year = random.Next(2022, 2024);
                    var countDays = DateTime.DaysInMonth(year, mounth);
                    var day = random.Next(1, countDays);
                    task.startTime = new DateTime(year, mounth, day);
                    task.endTime = new DateTime(year, mounth, day).AddDays(random.Next(2, 10));
                    project.tasks.Add(task);
                    allTasks.Add(task);
                }
            }
        }
        private void CreateColumns()
        {
            var startTimes = allTasks.Select(t => t.startTime).ToList();
            var endTimes = allTasks.Select(t => t.endTime).ToList();
            var minStartTime = startTimes.Min().Date;
            var maxEndTime = endTimes.Max().Date;
            int i = 0;
            while (minStartTime < maxEndTime)
            {
                MainGrid.ColumnDefinitions.Add(new ColumnDefinition() { MinWidth = 70 });
                var stackPanel = new StackPanel();
                var textBlock = new TextBlock();
                var formatDate = minStartTime.ToString("dd/MM/yyyy");
                stackPanel.Width = 70;
                textBlock.Text = formatDate;
                textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                textBlock.VerticalAlignment = VerticalAlignment.Center;
                stackPanel.HorizontalAlignment = HorizontalAlignment.Center;
                stackPanel.VerticalAlignment = VerticalAlignment.Center;
                stackPanel.SetValue(Grid.ColumnProperty, i + 1);
                stackPanel.SetValue(Grid.RowProperty, 0);
                stackPanel.Children.Add(textBlock);
                MainGrid.Children.Add(stackPanel);
                minStartTime = minStartTime.AddDays(1);
                i++;
            }
        }
        private void SetTasks()
        {
            var startTimes = allTasks.Select(t => t.startTime).ToList();
            var minTime = startTimes.Min().Date;
            for (int i = 0; i < projects.Count; i++)
            {
                var tasks = projects[i].tasks;
                for (int j = 0; j < tasks.Count; j++) 
                {
                    var task = tasks[j];
                    var startTime = task.startTime.Date;
                    var endTime = task.endTime.Date;
                    var border = new Border();
                    var workTime = (endTime - startTime).Days;
                    var difference = (startTime - minTime).Days;
                    if (task.status.id == 4)
                    {
                        var color = Color.FromArgb(
                            (byte)random.Next(0, 255),
                            (byte)random.Next(0, 255),
                            (byte)random.Next(0, 255),
                            (byte)random.Next(180, 200));
                        border.Background = new SolidColorBrush(color);
                    }
                    else
                        border.Background = task.status.color;
                    border.BorderThickness = new Thickness(2.0, 2.0, 2.0, 2.0);
                    var blackColor = Color.FromArgb(175, 0, 0, 0);
                    border.BorderBrush = new SolidColorBrush(blackColor);
                    border.Height = 40;
                    border.Margin = new Thickness(10, 10, 10, 10);
                    border.CornerRadius = new CornerRadius(5);
                    border.SetValue(Grid.ColumnProperty, difference + 1);
                    border.SetValue(Grid.ColumnSpanProperty, workTime);
                    border.SetValue(Grid.RowProperty, i + 1);
                    MainGrid.Children.Add(border);
                }

            }
        }
        private void CreateRows()
        {

            for (int i = 0; i < projects.Count; i++)
            {
                MainGrid.RowDefinitions.Add(new RowDefinition() { MaxHeight = 100 });
                MainGrid.Height += 80;
                var stackPanel = new StackPanel();
                var textBlock = new TextBlock();
                textBlock.Text = projects[i].name;
                textBlock.TextAlignment = TextAlignment.Left;
                textBlock.VerticalAlignment = VerticalAlignment.Center;
                stackPanel.Height = 70;
                stackPanel.VerticalAlignment = VerticalAlignment.Top;
                stackPanel.HorizontalAlignment = HorizontalAlignment.Center;
                stackPanel.SetValue(Grid.RowProperty, i + 1);
                stackPanel.SetValue(Grid.ColumnProperty, 0);
                stackPanel.Children.Add(textBlock);
                MainGrid.Children.Add(stackPanel);
            }

        }

        private void ButtonGenerateSchudule_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            timer.Start();
            MainGrid.Children.Clear();
            MainGrid.RowDefinitions.Clear();
            MainGrid.ColumnDefinitions.Clear();
            textBlockLoading.Visibility = Visibility.Visible;
            MainGrid.Children.Add(textBlockLoading);



        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            textBlockLoading.Visibility = Visibility.Hidden;
            timer.Stop();
            Refresh();
        }
    }
}
