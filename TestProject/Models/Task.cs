using System;

namespace TestProject.Models
{
    public class Task
    {
        public int id { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public Status status { get; set; } 


    }
}
